using Unity.Entities;

namespace UpdateGroups
{
    [UpdateAfter(typeof(ExecuteUpdateGroup))]
    public class CleanupUpdateGrpup
    {

    }
}