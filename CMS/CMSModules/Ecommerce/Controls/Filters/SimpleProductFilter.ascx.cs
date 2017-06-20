﻿using System;
using System.Linq;

using CMS.DataEngine;
using CMS.DocumentEngine.Web.UI;
using CMS.Ecommerce;
using CMS.SiteProvider;
using CMS.UIControls;


public partial class CMSModules_Ecommerce_Controls_Filters_SimpleProductFilter : CMSAbstractDataFilterControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the SQL condition for filtering the order list.
    /// </summary>
    public override string WhereCondition
    {
        get
        {
            base.WhereCondition = GetWhereCondition().ToString(true);
            return base.WhereCondition;
        }
        set
        {
            base.WhereCondition = value;
        }
    }

    #endregion


    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // If this control is associated with an UniGrid control, disable UniGrid's button
        UniGrid grid = FilteredControl as UniGrid;
        if (grid != null)
        {
            grid.HideFilterButton = true;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        btnShow.Text = GetString("general.filter");
        btnReset.Text = GetString("general.reset");
    }

    #endregion


    #region "Event Handlers"

    protected void btnShow_Click(object sender, EventArgs e)
    {
        UniGrid grid = FilteredControl as UniGrid;
        if (grid != null)
        {
            grid.ApplyFilter(sender, e);
        }

        RaiseOnFilterChanged();
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        UniGrid grid = FilteredControl as UniGrid;
        if (grid != null)
        {
            grid.Reset();
        }

        ResetFilter();
        RaiseOnFilterChanged();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Get where condition for unigrid
    /// </summary>
    /// <returns>Where condition</returns>
    private WhereCondition GetWhereCondition()
    {
        string productNameFilter = txtProductName.Text;

        // To display ONLY product - not product options
        var where = new WhereCondition().WhereTrue("SKUEnabled").And().WhereNull("SKUOptionCategoryID");

        if (!string.IsNullOrEmpty(productNameFilter))
        {
            // Alias for COM_SKU 
            var variants = new QuerySourceTable("COM_SKU", "variants");

            // Get IDs of products containing filter text; search among variants too
            var ids = new IDQuery<SKUInfo>()
                .Columns(new QueryColumn("COM_SKU.SKUID"))
                .Source(s => s.LeftJoin(variants, "COM_SKU.SKUID", "variants.SKUParentSKUID", 
                    new WhereCondition()
                        .WhereContains("variants.SKUName", productNameFilter)
                        .Or()
                        .WhereContains("variants.SKUNumber", productNameFilter)))
                .Where(
                    new WhereCondition()
                        .WhereContains("COM_SKU.SKUName", productNameFilter)
                        .Or()
                        .WhereContains("COM_SKU.SKUNumber", productNameFilter));

            // Add the condition
            where.Where(w => w.WhereIn("SKUID", ids));
        }

        where.Where(SKUInfoProvider.ProviderObject.AddSiteWhereCondition(string.Empty, SiteContext.CurrentSiteID, ECommerceSettings.ALLOW_GLOBAL_PRODUCTS, true));

        return where;
    }


    /// <summary>
    /// Resets filter to the default state.
    /// </summary>
    public override void ResetFilter()
    {
        txtProductName.Text = String.Empty;
    }

    #endregion
}
