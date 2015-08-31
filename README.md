# slinq

As for now the slinq is just an Experiment. It seems to be very promissing.

I have studied original LINQ code and it seems to be not optimized as it could be (lot of casts, abstract method invocations, few optimizations).

Idea:
1. Support only collections that wrap array, currently: List/ReadOnlyCollection/ImmutableArray. Maybe in the future: Queue, Stack & more
2. Get the wrapped array, use dynamically generated CIL to read those encapsulated fields. This requires ReflectionPermissionFlag.RestrictedMemberAccess
3. Create most commonly used iterators for array: Where, WhereSelect, SelectMany, Select, SelectWhere, Range, RangeSelect
4. For sorting create a copy-less sorter, c++ - template like sorter on the fly (no boxing, no struct copying, no virtual method invocation)
5. Iterate only over arrays
6. No casting
7. No virtual method invocations
8. Use structs wherever possible
9. Don't copy anything if it can be avoided
10. Use the knowledge of fixed/max size (i.e. array.Select(item => sth).ToArray()
11. Use the knowledge of fixed length (i.e. .Revert() .Last())
12. Use T4 for smart code generation
13. No 3rd party dependencies

and study, discover and reuse every single trick to tune perf

and ofc HAVE FUN