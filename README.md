# slinq

"The force is strong with this one" - Darth Vader on slinq

As for now the slinq is just an Experiment, PoC. It seems to be very promissing.

I have studied original LINQ code and it seems to be not as optimized as it could be (lot of casts, abstract method invocations, very  few optimizations).

Idea:
* Support only collections that wrap array, currently: List/ReadOnlyCollection/ImmutableArray. Maybe in the future: Queue, Stack & more
* Get the wrapped array, use dynamically generated CIL to read those encapsulated fields. This requires ReflectionPermissionFlag.RestrictedMemberAccess
* Create most commonly used iterators for array: Where, WhereSelect, SelectMany, Select, SelectWhere, Range, RangeSelect
* For sorting create a copy-less sorter, c++ - template like sorter on the fly (no boxing, no struct copying, no virtual method invocation)
* Iterate only over arrays. Avoid boundaries check if possible.
* No casting
* No virtual method invocations
* Use structs wherever possible
* Don't copy anything if it can be avoided
* Use the knowledge of fixed/max size (i.e. array.Select(item => sth).ToArray()
* Use the knowledge of fixed length (i.e. .Revert() .Last())
* Use T4 for smart code generation
* No 3rd party dependencies

and also study, discover and reuse every single trick to tune perf

**update**
I have stopped working on slinq some time ago. The reason was that I wanted to create something that could be used in any configuration, but with ReflectionPermissionFlag.RestrictedMemberAccess requirement it just impossible. The last step that I was planning to do was to get rid of lambda invocations by taking the expression's body IL code and reusing it. 
