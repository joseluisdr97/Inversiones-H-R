#pragma checksum "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "49d630c3b7219ef6a19d333e66b661d074cd58f0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LlamadasAjax_ObtenerCotizacionesCliente), @"mvc.1.0.view", @"/Views/LlamadasAjax/ObtenerCotizacionesCliente.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"49d630c3b7219ef6a19d333e66b661d074cd58f0", @"/Views/LlamadasAjax/ObtenerCotizacionesCliente.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fa98d2dfee75ae240d35d01ba65f82bc804d67c3", @"/Views/_ViewImports.cshtml")]
    public class Views_LlamadasAjax_ObtenerCotizacionesCliente : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
   Layout = null;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
 foreach (var item in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n        <td>");
#nullable restore
#line 5 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
       Write(item.Fecha);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 6 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
         if (item.Id_Estado != 3)
        {
            if (item.Id_Estado != 6)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>");
#nullable restore
#line 10 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
               Write(item.Fecha_Entrega_Solicitada);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 11 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
               Write(item.Direccion_Entrega);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 12 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
               Write(item.Telefono_Comunicacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 13 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <td>...</td>\r\n                <td>...</td>\r\n                <td>...</td>\r\n");
#nullable restore
#line 19 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }


#line default
#line hidden
#nullable disable
            WriteLiteral("            <td>");
#nullable restore
#line 21 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
           Write(item.Total);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 22 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <td>...</td>\r\n            <td>...</td>\r\n            <td>...</td>\r\n            <td>...</td>\r\n");
#nullable restore
#line 29 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("        <td>\r\n");
#nullable restore
#line 33 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
             if (item.Id_Estado == 3)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span class=\"label label-default\" style=\"background-color:yellow;color:black;margin-right:10px\"><a style=\"color:black\" href=\"/Home/MisCotizaciones?idestado=3\">");
#nullable restore
#line 35 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                                                                                                                                                                          Write(item.Estado.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></span>\r\n");
#nullable restore
#line 36 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 37 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
             if (item.Id_Estado == 6)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span class=\"label label-default\" style=\"background-color:green;color:black;margin-right:10px\"><a style=\"color:black\" href=\"/Home/MisCotizaciones?idestado=6\">");
#nullable restore
#line 39 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                                                                                                                                                                         Write(item.Estado.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></span>\r\n");
#nullable restore
#line 40 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
             if (item.Id_Estado == 1)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span class=\"label label-default\" style=\"background-color:blueviolet;color:black;margin-right:10px\"><a style=\"color:black\" href=\"/Home/MisCotizaciones?idestado=1\">");
#nullable restore
#line 43 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                                                                                                                                                                              Write(item.Estado.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></span>\r\n");
#nullable restore
#line 44 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 45 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
             if (item.Id_Estado == 2)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span class=\"label label-default\" style=\"background-color:chocolate;color:black;margin-right:10px\"><a style=\"color:black\" href=\"/Home/MisCotizaciones?idestado=2\">");
#nullable restore
#line 47 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                                                                                                                                                                             Write(item.Estado.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></span>\r\n");
#nullable restore
#line 48 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
             if (item.Id_Estado == 4)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span class=\"label label-default\" style=\"background-color:darkorange;color:black;margin-right:10px\"><a style=\"color:black\" href=\"/Home/MisCotizaciones?idestado=4\">Construido</a></span>\r\n");
#nullable restore
#line 52 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
             if (item.Id_Estado == 5)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span class=\"label label-default\" style=\"background-color:Highlight;color:black;margin-right:10px\"><a style=\"color:black\" href=\"/Home/MisCotizaciones?idestado=5\">Recibido</a></span>\r\n");
#nullable restore
#line 56 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </td>\r\n");
#nullable restore
#line 58 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
         if (item.Id_Estado != 3 && item.Id_Estado != 6)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <td>\r\n");
#nullable restore
#line 61 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                  string a = "";
                    a = Convert.ToString(item.Avance + "%");
                

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"progress\">\r\n                    <div class=\"progress-bar bg-warning\" role=\"progressbar\"");
            BeginWriteAttribute("style", " style=\"", 2732, "\"", 2749, 2);
            WriteAttributeValue("", 2740, "width:", 2740, 6, true);
#nullable restore
#line 65 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
WriteAttributeValue(" ", 2746, a, 2747, 2, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("aria-valuenow", " aria-valuenow=\"", 2750, "\"", 2778, 1);
#nullable restore
#line 65 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
WriteAttributeValue("", 2766, item.Avance, 2766, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" aria-valuemin=\"0\" aria-valuemax=\"100\">");
#nullable restore
#line 65 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                                                                                                                                                            Write(a);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n                </div>\r\n            </td>\r\n");
#nullable restore
#line 68 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <td>...</td>\r\n");
#nullable restore
#line 72 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <td>\r\n            <a class=\"verdetallecotizacion btn btn-success\" data-toggle=\"modal\" data-target=\".bd-example-modal-lg2\" data-idcotizacion=\"");
#nullable restore
#line 75 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                                                                                                                                  Write(item.Id_Cotizacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-eye-open\"></span> Ver productos</a>\r\n");
#nullable restore
#line 76 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
             if (item.Id_Estado == 1 || item.Id_Estado == 3 || item.Id_Estado == 6)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <a class=\"eliminarcotizacion btn btn-danger\" data-toggle=\"modal\" data-target=\".bd-example-modal-lg1\" data-idcotizacion=\"");
#nullable restore
#line 78 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                                                                                                                                   Write(item.Id_Cotizacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-trash\"></span></a>\r\n");
#nullable restore
#line 79 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 80 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
             if (item.Id_Estado == 6)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <button type=\"button\" class=\"registrarcomopedido btn btn-primary\" style=\"margin-top:2px\" data-toggle=\"modal\" data-target=\"#exampleModal\" data-idcotizacion=\"");
#nullable restore
#line 82 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
                                                                                                                                                                       Write(item.Id_Cotizacion);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"glyphicon glyphicon-save\"></span> Registrar como pedido</button>\r\n");
#nullable restore
#line 83 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </td>\r\n    </tr>\r\n");
#nullable restore
#line 86 "C:\Users\josel\source\repos\MADERERA_HRS\MADERERA_HRS\Views\LlamadasAjax\ObtenerCotizacionesCliente.cshtml"
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
