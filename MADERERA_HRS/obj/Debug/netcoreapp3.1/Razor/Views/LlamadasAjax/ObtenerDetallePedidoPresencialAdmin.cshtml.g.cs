#pragma checksum "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fede489287d2f645808614ac6ebae0f0577fcb4f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LlamadasAjax_ObtenerDetallePedidoPresencialAdmin), @"mvc.1.0.view", @"/Views/LlamadasAjax/ObtenerDetallePedidoPresencialAdmin.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fede489287d2f645808614ac6ebae0f0577fcb4f", @"/Views/LlamadasAjax/ObtenerDetallePedidoPresencialAdmin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa98d2dfee75ae240d35d01ba65f82bc804d67c3", @"/Views/_ViewImports.cshtml")]
    public class Views_LlamadasAjax_ObtenerDetallePedidoPresencialAdmin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
  Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("<!--AQUI ME QUEDO AUN FALTA REVISAR TODO ESTO-->\r\n");
#nullable restore
#line 3 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
 foreach (var item in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td>");
#nullable restore
#line 6 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
       Write(item.Producto);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 7 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
       Write(item.Medidas);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>\r\n            ");
#nullable restore
#line 9 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
       Write(item.Cantidad);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 10 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
             if (item.Pedido_Presencial.Id_Estado != 5)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a class=\"actualizarcantidaddetallepedido btn btn-warning\" data-idpedido=\"");
#nullable restore
#line 12 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
                                                                                     Write(item.Id_Pedido_Presencial);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-iddetallepedido=\"");
#nullable restore
#line 12 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
                                                                                                                                       Write(item.Id_Detalle_Pedido_Presencial);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-refresh\"></span></a>\r\n");
#nullable restore
#line 13 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </td>\r\n            <td>");
#nullable restore
#line 15 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
           Write(item.Precio);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n            <td>");
#nullable restore
#line 16 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
           Write(item.Sub_Total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 17 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
       Write(item.Descripcion);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>\r\n");
#nullable restore
#line 19 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
             if (item.Pedido_Presencial.Id_Estado != 5)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a class=\"eliminaritemdetallepedido btn btn-danger\" data-idpedido=\"");
#nullable restore
#line 21 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
                                                                              Write(item.Id_Pedido_Presencial);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-iddetallepedido=\"");
#nullable restore
#line 21 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
                                                                                                                                Write(item.Id_Detalle_Pedido_Presencial);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-trash\"></span>");
            WriteLiteral("</a>\r\n");
#nullable restore
#line 22 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(" <label>...</label>");
#nullable restore
#line 24 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </td>\r\n    </tr>\r\n");
#nullable restore
#line 27 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerDetallePedidoPresencialAdmin.cshtml"
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
