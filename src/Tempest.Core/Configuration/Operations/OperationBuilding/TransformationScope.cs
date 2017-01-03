namespace Tempest.Core.Configuration.Operations.OperationBuilding
{
    public enum TransformationScope
    {
        /// <summary>
        /// Performs transformation defined here prior to global transformations
        /// </summary>
        BeforeGlobals,

        /// <summary>
        /// Performs transformations defined here after global transformation
        /// </summary>
        AfterGlobals
    }
}