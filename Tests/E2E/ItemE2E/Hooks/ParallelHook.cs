using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]

namespace ItemE2E.Hooks
{
    [Binding]
    public sealed class ParallelHook
    {
    }
}