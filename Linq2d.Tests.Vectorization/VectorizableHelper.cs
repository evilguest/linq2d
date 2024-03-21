using Mono.Linq.Expressions;
namespace Linq2d.Tests.Vectorization;

public static class VectorizableHelper
{
    public static string Report(this IVectorizable v)
        => string.Join(";\n", v.VectorizationResults.Where(vr => !vr.Success)
                .Select(vr => $"{vr.Reason} with step {vr.Step} due to:\n{vr.BlockedBy.ToCSharpCode()}"));
    public static int MaxSuccessfulStep(this IVectorizable v) => v.VectorizationResults.Where(vr => vr.Success).Select(vr => vr.Step).Max();
}
