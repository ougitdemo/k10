using CMS.Newsletters;
using CMS.UIControls;
using CMS.WebAnalytics;
using System;
using System.Linq;
using CMS.Helpers;

public partial class CMSApp_CMSModules_KenticoTrainingDataGenerator_Default : CMSDeskPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var allIssues = IssueInfoProvider.GetIssues().Columns("IssueID, IssueSubject, IssueMailoutTime, IssueVariantOfIssueID").ToList();

            ddEmailIssues.DataSource = allIssues.Where(m => m.IssueMailoutTime != DateTime.MinValue);
            ddEmailIssues.DataTextField = "IssueSubject";
            ddEmailIssues.DataValueField = "IssueID";
            ddEmailIssues.DataBind();

            ddEmail2Issues.DataSource = allIssues.Where(m => m.IssueMailoutTime != DateTime.MinValue);
            ddEmail2Issues.DataTextField = "IssueSubject";
            ddEmail2Issues.DataValueField = "IssueID";
            ddEmail2Issues.DataBind();

            var abTests = CMS.OnlineMarketing.ABTestInfoProvider.GetABTests().Columns("ABTestID, ABTestName");
            ddABSelector.DataSource = abTests;
            ddABSelector.DataTextField = "ABTestName";
            ddABSelector.DataValueField = "ABTestID";
            ddABSelector.DataBind();

            var conversions = ConversionInfoProvider.GetConversions().Columns("ConversionID, ConversionName");
            ddConversionSelector.DataSource = conversions;
            ddConversionSelector.DataTextField = "ConversionName";
            ddConversionSelector.DataValueField = "ConversionID";
            ddConversionSelector.DataBind();

            // A/B Test e-mails
            var issuesWithVariants = allIssues.Where(m => allIssues.Where(s => s.IssueVariantOfIssueID != 0).Select(s => s.IssueVariantOfIssueID).Distinct().Contains(m.IssueID)).Distinct();
            ddEmailIssuesABTest.DataSource = issuesWithVariants;
            ddEmailIssuesABTest.DataTextField = "IssueSubject";
            ddEmailIssuesABTest.DataValueField = "IssueID";
            ddEmailIssuesABTest.DataBind();
        }
    }

    protected void btnInit_Click(object sender, EventArgs e)
    {
        try
        {
            var result = KenticoTrainingDataGenerator.Generator.Initialize();
            if (result)
            {
                ShowConfirmation("Initialization successfully completed.");
            }
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    protected void btnEmail_Click(object sender, EventArgs e)
    {
        try
        {
            var issueId = Convert.ToInt32(ddEmailIssues.SelectedValue);
            if (issueId > 0)
            {
                KenticoTrainingDataGenerator.Generator.Exercise1(issueId);

                ShowConfirmation("Email data generated.");
                return;
            }

            ShowError("Email #1 issue must be selected");
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }

    }
    protected void btnEmail2_Click(object sender, EventArgs e)
    {
        try
        {
            var issueId = Convert.ToInt32(ddEmail2Issues.SelectedValue);
            if (issueId > 0)
            {
                KenticoTrainingDataGenerator.Generator.Exercise2(issueId);

                ShowConfirmation("Email #2 data generated.");
                return;
            }

            ShowError("Email issue must be selected");
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }
    protected void btnABtest_Click(object sender, EventArgs e)
    {
        try
        {
            var abTestId = Convert.ToInt32(ddABSelector.SelectedValue);
            var conversionId = Convert.ToInt32(ddConversionSelector.SelectedValue);

            var testNotYetStarted =
                CMS.OnlineMarketing.ABTestInfoProvider.GetABTests()
                    .Column("ABTestID")
                    .WhereEquals("ABTestID", abTestId)
                    .WhereNull("ABTestOpenFrom")
                    .TopN(1)
                    .SingleOrDefault();
            if (testNotYetStarted != null)
            {
                ShowError("AB test must be started first.");
                return;
            }

            if (abTestId > 0)
            {
                KenticoTrainingDataGenerator.Generator.Exercise3(abTestId, conversionId, 5);

                ShowConfirmation("AB test data generated.");
                return;
            }

            ShowError("AB test must be selected");
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }


    protected void btnEmailAbTest_OnClick(object sender, EventArgs e)
    {
        try
        {
            var issueId = Convert.ToInt32(ddEmailIssuesABTest.SelectedValue);
            if (issueId > 0)
            {

                KenticoTrainingDataGenerator.Generator.Exercise4(issueId);

                ShowChangesSaved();
            }
            else
            {
                ShowError("E-mail A/B Test issue must be selected");
            }
        }

        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }
}