# slinq

As for now the slinq is just an Experiment.

I have studied original LINQ code and it seems to be not optimized as it could be (lot of casts, abstract method invocations, few optimizations).

My current vision:
#1. Create dedicated iterators for Array
#2. Iterators should be value types to reduce GC pressure
#3. Add support for List, ReadOnlyCollections and ImmutableCollections with some nasty hack that is going to get the underlying table and reuse exitign code ;)
