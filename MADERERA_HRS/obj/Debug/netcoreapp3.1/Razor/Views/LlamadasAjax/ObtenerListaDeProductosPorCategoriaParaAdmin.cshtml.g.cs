#pragma checksum "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "042222daec80d8c40a70a254046181bc6c0d2bdb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LlamadasAjax_ObtenerListaDeProductosPorCategoriaParaAdmin), @"mvc.1.0.view", @"/Views/LlamadasAjax/ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml")]
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
#nullable restore
#line 1 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\_ViewImports.cshtml"
using MADERERA_HRS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\_ViewImports.cshtml"
using MADERERA_HRS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"042222daec80d8c40a70a254046181bc6c0d2bdb", @"/Views/LlamadasAjax/ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa98d2dfee75ae240d35d01ba65f82bc804d67c3", @"/Views/_ViewImports.cshtml")]
    public class Views_LlamadasAjax_ObtenerListaDeProductosPorCategoriaParaAdmin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
  Layout = null;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
 foreach (var item in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td><img style=\"width:40px;height:55px\"");
            BeginWriteAttribute("src", " src=\"", 111, "\"", 129, 1);
#nullable restore
#line 6 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
WriteAttributeValue("", 117, item.Imagen, 117, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" /> ");
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 7 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
       Write(item.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 8 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
       Write(item.Categoria.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 9 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
       Write(item.Medidas);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 10 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
       Write(item.Precio);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 11 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
       Write(item.Descripcion);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>\r\n            <a class=\"btn btn-info\"");
            BeginWriteAttribute("href", " href=\"", 377, "\"", 426, 2);
            WriteAttributeValue("", 384, "/ProductoAdmin/Editar?id=", 384, 25, true);
#nullable restore
#line 13 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
WriteAttributeValue("", 409, item.Id_Producto, 409, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><span class=\"glyphicon glyphicon-pencil\"></span>Editar</a>\r\n            <a class=\"eliminar btn btn-danger\" data-idproducto=\"");
#nullable restore
#line 14 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
                                                           Write(item.Id_Producto);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-trash\"></span>Eliminar</a>\r\n        </td>\r\n    </tr>\r\n");
#nullable restore
#line 17 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerListaDeProductosPorCategoriaParaAdmin.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
