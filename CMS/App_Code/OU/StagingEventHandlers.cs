
using CMS;
using CMS.DataEngine;
using CMS.DocumentEngine;
using CMS.PortalEngine;
using CMS.Synchronization;

[assembly: RegisterModule(typeof(StagingEventHandlers))]
public class StagingEventHandlers : Module
{
    public StagingEventHandlers() : base(nameof(StagingEventHandlers))
    {
    }
    protected override void OnInit()
    {
        base.OnInit();
        
        StagingEvents.GetChildProcessingType.Execute += Staging_GetChildProcessingType;
    }

    private void Staging_GetChildProcessingType(object sender, StagingChildProcessingTypeEventArgs e)
    {
        if (e.ParentObjectType != TreeNode.OBJECT_TYPE)
            return;

        if (e.ObjectType == PageTemplateInfo.OBJECT_TYPE ||
            e.ObjectType == PageTemplateCategoryInfo.OBJECT_TYPE)
            e.ProcessingType = IncludeToParentEnum.None;
    }
}