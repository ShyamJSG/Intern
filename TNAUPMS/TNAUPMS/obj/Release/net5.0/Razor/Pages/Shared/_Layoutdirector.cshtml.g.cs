#pragma checksum "D:\Intern\TNAUPMS\TNAUPMS\Pages\Shared\_Layoutdirector.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "93245e74a5962b15afb10ab33b4f5532c672a4232071c02cfbc26cfd5c6a07fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(TNAUPMS.Pages.Shared.Pages_Shared__Layoutdirector), @"mvc.1.0.view", @"/Pages/Shared/_Layoutdirector.cshtml")]
namespace TNAUPMS.Pages.Shared
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Intern\TNAUPMS\TNAUPMS\Pages\_ViewImports.cshtml"
using TNAUPMS

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"93245e74a5962b15afb10ab33b4f5532c672a4232071c02cfbc26cfd5c6a07fa", @"/Pages/Shared/_Layoutdirector.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"0306b3b36d9f566ba79880baa6a88d0a917d4af0873c1c083baedbd576057f60", @"/Pages/_ViewImports.cshtml")]
    #nullable restore
    public class Pages_Shared__Layoutdirector : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "/index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("go-to-page"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("background-color: #dff8e5;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!doctype html>\n<html lang=\"en\">\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "93245e74a5962b15afb10ab33b4f5532c672a4232071c02cfbc26cfd5c6a07fa4702", async() => {
                WriteLiteral(@"
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>TNAU - Project Monitoring System</title>

    <link rel=""shortcut icon"" href=""assets/media/image/favicon.ico"" />
    <link rel=""stylesheet"" href=""vendors/bundle.css"" type=""text/css"">
    <link rel=""stylesheet"" href=""vendors/datepicker/daterangepicker.css"">
    <link rel=""stylesheet"" href=""vendors/slick/slick.css"">
    <link rel=""stylesheet"" href=""vendors/slick/slick-theme.css"">
    <link rel=""stylesheet"" href=""vendors/vmap/jqvmap.min.css"">
    <link rel=""stylesheet"" href=""assets/css/app.min.css"" type=""text/css"">
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "93245e74a5962b15afb10ab33b4f5532c672a4232071c02cfbc26cfd5c6a07fa6417", async() => {
                WriteLiteral(@"
    <div class=""page-loader"">
        <div class=""spinner-border""></div>
    </div>

    <div class=""header"" style=""background-color: #1c471c;"">
        <div class=""header-logo"" style=""background-color: #1c471c; "">
                <img class=""large-logo"" src=""assets/media/image/logo.png"" style=""width:90px;"" alt=""image"">
        </div>
        <div class=""header-body"">
            <div class=""header-body-left"">
                <h3 class=""page-title"" style=""color:#fff;"">Project Monitoring System</h3>
                <nav aria-label=""breadcrumb"">
                    <ol class=""breadcrumb"">
                        <li class=""breadcrumb-item""><a href=""#"" style=""color:#fff;"">TAMILNADU AGRICULTURE UNIVERSITY</a></li>
                    </ol>
                </nav>
            </div>
            <div class=""header-body-right"">
                <ul class=""navbar-nav"">
                    <li class=""nav-item dropdown"">
                        <a href=""#"" class=""nav-link bg-none"">
                            <div>
    ");
                WriteLiteral(@"                            <figure class=""avatar avatar-state-success avatar-sm"">
                                    <img src=""assets/media/image/avatar.jpg"" class=""rounded-circle"" alt=""image"">
                                </figure>
                            </div>
                        </a>
                    </li>
                </ul>
                <div class=""d-flex align-items-center"">
                    <div class=""d-xl-none d-lg-none d-sm-block navigation-toggler"">
                        <a href=""#"">
                            <i class=""ti-menu""></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class=""navigation"">
        <div class=""navigation-icon-menu"">
            <ul>
                <li class=""active"" data-toggle=""tooltip"" title=""Menu"">
                    <a href=""#navigationmain"" title=""Menu"">
                        <i class=""icon ti-pie-chart""></i>
                        <span class=""badge");
                WriteLiteral(@" badge-warning"">2</span>
                    </a>
                </li>
            </ul>
            <ul>
                 <li data-toggle=""tooltip"" title=""Settings"">
                    <a href=""#navigationsettings"">
                        <i class=""icon ti-settings""></i>
                    </a>
                </li>
                <li data-toggle=""tooltip"" title=""Logout"">
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "93245e74a5962b15afb10ab33b4f5532c672a4232071c02cfbc26cfd5c6a07fa9311", async() => {
                    WriteLiteral("\n                        <i class=\"icon ti-power-off\"></i>\n                    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                </li>
            </ul>
        </div>
        <div class=""navigation-menu-body"">
            <ul id=""navigationmain"" class=""navigation-active"">
                <li class=""navigation-divider"">MENU</li>
                <li><a href=""homedirector"">Home</a></li>
                <li><a href=""projects"">Projects</a></li>
                <li><a href=""projects"">Project Report</a></li>
              
            </ul>
             <ul id=""navigationsettings"">
                <li class=""navigation-divider"">SETTINGS</li>
                <li><a href=""#"">Profile </a></li>
                <li><a href=""#"">Change Password </a></li>
            </ul>
        </div>
    </div>


    <main class=""main-content"">
        ");
                Write(
#nullable restore
#line 99 "D:\Intern\TNAUPMS\TNAUPMS\Pages\Shared\_Layoutdirector.cshtml"
         RenderBody()

#line default
#line hidden
#nullable disable
                );
                WriteLiteral(@"

    </main>
    <div class=""fixed-bottom"" tabindex=""-1"" style=""margin: 0px; background-color: #1c471c;"">
        <div class=""row"" style=""margin:0px;"">
            <div class=""col-12"" style=""text-align:center; margin:0px;"">
                <div style=""margin:0px;"">
                    <p style=""font-size: 12px; margin: 2px; padding: 2px; color: #fff;"">&copy; 2022 TNAU.   Powerd By <a style=""color: #fff;"" target=""_blank"" href=""https://dhanvanthtechservices.com"">Dhanvanth Tech Services LLP</a></p>
                </div>
            </div>
        </div>


    </div>

    <script src=""vendors/bundle.js""></script>
    <script src=""vendors/charts/chartjs/chart.min.js""></script>
    <script src=""vendors/circle-progress/circle-progress.min.js""></script>
    <script src=""vendors/charts/peity/jquery.peity.min.js""></script>
    <script src=""assets/js/examples/charts/peity.js""></script>
    <script src=""vendors/datepicker/daterangepicker.js""></script>
    <script src=""vendors/slick/slick.min.js""></script>
    <script s");
                WriteLiteral(@"rc=""vendors/vmap/jquery.vmap.min.js""></script>
    <script src=""vendors/vmap/maps/jquery.vmap.usa.js""></script>
    <script src=""assets/js/examples/vmap.js""></script>
    <script src=""assets/js/site.js""></script>
    <script src=""assets/js/examples/dashboard.js""></script>
    <div class=""colors"">
        <div class=""bg-primary""></div>
        <div class=""bg-primary-bright""></div>
        <div class=""bg-secondary""></div>
        <div class=""bg-secondary-bright""></div>
        <div class=""bg-info""></div>
        <div class=""bg-info-bright""></div>
        <div class=""bg-success""></div>
        <div class=""bg-success-bright""></div>
        <div class=""bg-danger""></div>
        <div class=""bg-danger-bright""></div>
        <div class=""bg-warning""></div>
        <div class=""bg-warning-bright""></div>
    </div>
    <script src=""assets/js/app.min.js""></script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</html>\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
