using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWeek8.MVC.Models;

namespace TestWeek8.MVC.TagHelpers
{
    public class AddressDivTagHelper: TagHelper
    {
        public Address Address { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("itemscope itemtype", "http://schema.org/Organization");
            output.Content.SetHtmlContent(
            $@"<label><strong>Dove trovarci: </strong></label><br />
            <address itemprop=""address""><br/>
            <span itemprop=""streetAddress"">{Address.StreetAddress}</span><br/>
            <span itemprop=""addressLocality"">{Address.City}</span><br/>
            <span itemprop=""streetRegion"">{Address.Region}</span><br/>
            <span itemprop=""postalCode"">{Address.PostalCode}</span>");
            output.Attributes.SetAttribute("class", "row col-5");
        }
    }
}
