#pragma checksum "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f12e6afa35a18ca09e8b59df7e5d1bdc5ce0b111"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LlamadasAjax_ObtenerPedidosCotizadosEnConstruccionAdmin), @"mvc.1.0.view", @"/Views/LlamadasAjax/ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f12e6afa35a18ca09e8b59df7e5d1bdc5ce0b111", @"/Views/LlamadasAjax/ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa98d2dfee75ae240d35d01ba65f82bc804d67c3", @"/Views/_ViewImports.cshtml")]
    public class Views_LlamadasAjax_ObtenerPedidosCotizadosEnConstruccionAdmin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
   Layout = null;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
 foreach (var item in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td>");
#nullable restore
#line 5 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
       Write(item.Usuario.DNI);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 6 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
       Write(item.Usuario.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 7 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
       Write(item.Telefono_Comunicacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 8 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
       Write(item.Fecha);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 9 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
       Write(item.Fecha_Entrega_Solicitada);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 10 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
       Write(item.Direccion_Entrega);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 11 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
       Write(item.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>\r\n");
#nullable restore
#line 13 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
              string a = "";
                a = Convert.ToString(item.Avance + "%");
            

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"progress\">\r\n                <div class=\"progress-bar bg-warning\" role=\"progressbar\"");
            BeginWriteAttribute("style", " style=\"", 559, "\"", 576, 2);
            WriteAttributeValue("", 567, "width:", 567, 6, true);
#nullable restore
#line 17 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
WriteAttributeValue(" ", 573, a, 574, 2, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("aria-valuenow", " aria-valuenow=\"", 577, "\"", 605, 1);
#nullable restore
#line 17 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
WriteAttributeValue("", 593, item.Avance, 593, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" aria-valuemin=\"0\" aria-valuemax=\"100\">");
#nullable restore
#line 17 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
                                                                                                                                                        Write(a);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            </div>\r\n            <br />\r\n            <a class=\"actualizarporcentajeconstrucion btn btn-warning\" data-idcotizacion=\"");
#nullable restore
#line 20 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
                                                                                     Write(item.Id_Cotizacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-refresh\"></span></a>\r\n        </td>\r\n        <td>\r\n            <button type=\"button\" class=\"verdetallecotizacion btn btn-success\" data-toggle=\"modal\" data-target=\".bd-example-modal-xl\" data-idcotizacion=\"");
#nullable restore
#line 23 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
                                                                                                                                                    Write(item.Id_Cotizacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"fa fa-eye\"></span> ver productos</button>\r\n");
            WriteLiteral("            <a class=\"finalizarconstruccion btn btn-warning\" data-toggle=\"modal\" data-target=\".modal-dialog modal-xl1\" data-idcotizacion=\"");
#nullable restore
#line 25 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
                                                                                                                                     Write(item.Id_Cotizacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-play\"></span> Finalizar construcci??n</a>\r\n            <a class=\"eliminarcotizacion btn btn-danger\" data-idcotizacion=\"");
#nullable restore
#line 26 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
                                                                       Write(item.Id_Cotizacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-trash\"></span></a>\r\n        </td>\r\n    </tr>\r\n");
#nullable restore
#line 29 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerPedidosCotizadosEnConstruccionAdmin.cshtml"
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
