#pragma checksum "C:\Users\esme_\OneDrive\Documentos\SEPTIMO SEMESTRE\PROGRAMACION-WEB\Practicas\PrograWeb-U4\U4-ControlUsuario\u4-2_RolesUsuario\Views\Home\VerAlumnos.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "294efd1b245b900e6df311e2d31be377d7da6592"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_VerAlumnos), @"mvc.1.0.view", @"/Views/Home/VerAlumnos.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"294efd1b245b900e6df311e2d31be377d7da6592", @"/Views/Home/VerAlumnos.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0016b6767730c936e45b5440b0aea7bd14cb65d3", @"/Views/Home/_ViewImports.cshtml")]
    public class Views_Home_VerAlumnos : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<u4_2_RolesUsuario.Models.Alumno>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Home/AgregarAlumno"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("b"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("~/Home/EliminarAlumno"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\esme_\OneDrive\Documentos\SEPTIMO SEMESTRE\PROGRAMACION-WEB\Practicas\PrograWeb-U4\U4-ControlUsuario\u4-2_RolesUsuario\Views\Home\VerAlumnos.cshtml"
  
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


<div class=""central"">
    <h2 class=""titulo"">Alumnos inscritos</h2>
    <table class=""tabla"">
        <colgroup>
            <col style=""width:20%"" />
            <col style=""width:60%"" />
            <col style=""width:20%"" />
        </colgroup>
        <caption class=""agregar"" >
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "294efd1b245b900e6df311e2d31be377d7da65925464", async() => {
                WriteLiteral("Agregar");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("<br />\r\n        </caption>\r\n        <thead>\r\n            <tr>\r\n                <th>Grupo</th>\r\n                <th>Nombre</th>\r\n                <th>Accion</th>\r\n            </tr>\r\n        </thead>\r\n");
#nullable restore
#line 27 "C:\Users\esme_\OneDrive\Documentos\SEPTIMO SEMESTRE\PROGRAMACION-WEB\Practicas\PrograWeb-U4\U4-ControlUsuario\u4-2_RolesUsuario\Views\Home\VerAlumnos.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tbody>\r\n                <tr>\r\n                    <td style=\"text-align:center;\">");
#nullable restore
#line 31 "C:\Users\esme_\OneDrive\Documentos\SEPTIMO SEMESTRE\PROGRAMACION-WEB\Practicas\PrograWeb-U4\U4-ControlUsuario\u4-2_RolesUsuario\Views\Home\VerAlumnos.cshtml"
                                              Write(item.Grupo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"nombre\" style=\"text-align:center;\">");
#nullable restore
#line 32 "C:\Users\esme_\OneDrive\Documentos\SEPTIMO SEMESTRE\PROGRAMACION-WEB\Practicas\PrograWeb-U4\U4-ControlUsuario\u4-2_RolesUsuario\Views\Home\VerAlumnos.cshtml"
                                                             Write(item.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td id=\"acc\" style=\"text-align:center;\">\r\n                        <a class=\"link\"");
            BeginWriteAttribute("href", " href=\"", 1003, "\"", 1039, 2);
            WriteAttributeValue("", 1010, "/Home/EditarAlumno/", 1010, 19, true);
#nullable restore
#line 34 "C:\Users\esme_\OneDrive\Documentos\SEPTIMO SEMESTRE\PROGRAMACION-WEB\Practicas\PrograWeb-U4\U4-ControlUsuario\u4-2_RolesUsuario\Views\Home\VerAlumnos.cshtml"
WriteAttributeValue("", 1029, item.Id, 1029, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Editar</a>\r\n                        <a class=\"link\"");
            BeginWriteAttribute("href", " href=\"", 1092, "\"", 1128, 3);
            WriteAttributeValue("", 1099, "javascript:eliminar(", 1099, 20, true);
#nullable restore
#line 35 "C:\Users\esme_\OneDrive\Documentos\SEPTIMO SEMESTRE\PROGRAMACION-WEB\Practicas\PrograWeb-U4\U4-ControlUsuario\u4-2_RolesUsuario\Views\Home\VerAlumnos.cshtml"
WriteAttributeValue("", 1119, item.Id, 1119, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1127, ")", 1127, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Eliminar</a>\r\n                    </td>\r\n                </tr>\r\n            </tbody>\r\n");
#nullable restore
#line 39 "C:\Users\esme_\OneDrive\Documentos\SEPTIMO SEMESTRE\PROGRAMACION-WEB\Practicas\PrograWeb-U4\U4-ControlUsuario\u4-2_RolesUsuario\Views\Home\VerAlumnos.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </table>\r\n\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "294efd1b245b900e6df311e2d31be377d7da65929776", async() => {
                WriteLiteral("\r\n        <input type=\"hidden\" name=\"Id\" id=\"alumnoid\" />\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

    <script>
        function eliminar(id) {
            if (confirm('¿Está seguro de eliminar el alumno seleccionado?')) {
                document.getElementById(""alumnoid"").value = id;
                document.querySelector(""form"").submit();
            }
        }
    </script>
</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<u4_2_RolesUsuario.Models.Alumno>> Html { get; private set; }
    }
}
#pragma warning restore 1591
