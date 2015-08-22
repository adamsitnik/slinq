using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using NUnit.Framework;
using Slinq.Utils;

namespace Slinq.Tests.Security
{
    [TestFixture]
    public class SandboxPermissionsTests
    {
        [SuppressMessage("Microsoft.Security", "CA2103:ReviewImperativeSecurity" , 
            Justification = "I really need that"), Test]
        public void ShouldWorkEvenWithLowestPossiblePermissions()
        {
            // based on: https://msdn.microsoft.com/en-us/library/bb384237(v=vs.110).aspx

            Evidence evidence = new Evidence();
            evidence.AddHostEvidence(new Zone(SecurityZone.Internet));
            PermissionSet permissionSet = new NamedPermissionSet("Internet", SecurityManager.GetStandardSandbox(evidence));
            permissionSet.SetPermission(new ReflectionPermission(ReflectionPermissionFlag.RestrictedMemberAccess));
            AppDomainSetup appDomainSetup = new AppDomainSetup
            {
                ApplicationBase = "."
            };

            AppDomain sandbox = AppDomain.CreateDomain("Sandbox", evidence, appDomainSetup, permissionSet, null);
            CrossDomain crossDomain = (CrossDomain)sandbox.CreateInstanceAndUnwrap(typeof(CrossDomain).Assembly.FullName, typeof(CrossDomain).FullName);

            Assert.AreEqual(3, crossDomain.RunArrayProvider());
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "On purpose")]
    public class CrossDomain : MarshalByRefObject
    {
        public int RunArrayProvider()
        {
            var list = new List<int> { 1, 2, 3 };

            ArrayProvider<int>.GetWrappedArray(list);

            return list.Count;
        }
    }
}