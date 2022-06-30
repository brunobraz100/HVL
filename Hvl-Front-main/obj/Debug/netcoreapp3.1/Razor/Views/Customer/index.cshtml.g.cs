#pragma checksum "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d3d01c68c461807c644cdcdd230f21825eff9c5e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_index), @"mvc.1.0.view", @"/Views/Customer/index.cshtml")]
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
#line 1 "/Users/felipebotelho/Projects/Hvl-Front/Views/_ViewImports.cshtml"
using hvlFront;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/felipebotelho/Projects/Hvl-Front/Views/_ViewImports.cshtml"
using hvlFront.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3d01c68c461807c644cdcdd230f21825eff9c5e", @"/Views/Customer/index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"33012d7e4a89a70e64c4147e5337f46a90f9cb60", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Customer_index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CustomerViewModel>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
  
    ViewData["Title"] = "Clientes";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<link rel=""stylesheet"" type=""text/css"" href=""https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css"">

<div class=""card mb-4"">
    <br />
    <div class=""card-header"">
        <i class=""fas fa-users me-1""></i>
        Clientes
        <a  class=""btn btn-primary btn-floating""");
            BeginWriteAttribute("href", " href=\"", 361, "\"", 398, 1);
#nullable restore
#line 14 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
WriteAttributeValue("", 368, Url.Action("Add", "Customer"), 368, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
            <i class=""fa-solid fa-plus""></i>
            Novo cliente
        </a>
    </div>
    <div class=""card-body table-responsive"">
        <table id=""dataTable"" class=""table"">
            <thead>
                <tr>
                    <td>CNPJ</td>
                    <td>Razão Social</td>
                    <td>E-mail</td>
                    <td>Ações</td>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 30 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
                 foreach (CustomerViewModel customer in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td>");
#nullable restore
#line 33 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
                       Write(customer.NmCnpj);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>");
#nullable restore
#line 34 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
                       Write(customer.DsRasaoSocial);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>");
#nullable restore
#line 35 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
                       Write(customer.DsEmail);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>\n                            <a");
            BeginWriteAttribute("href", " href=\"", 1159, "\"", 1235, 1);
#nullable restore
#line 37 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
WriteAttributeValue("", 1166, Url.Action("Edit", "Customer", new {IdCliente = customer.IdCliente}), 1166, 69, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\" data-toggle=\"tooltip\" title=\"Editar\">\n                                <i class=\"fa-solid fa-pen\"></i>\n                            </a>\n                            <a");
            BeginWriteAttribute("href", " href=\"", 1426, "\"", 1504, 1);
#nullable restore
#line 40 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
WriteAttributeValue("", 1433, Url.Action("index", "Documents", new {IdCliente = customer.IdCliente}), 1433, 71, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-warning\" data-toggle=\"tooltip\" title=\"Documentos\">\n                                <i class=\"fa-solid fa-file\"></i>\n                            </a>\n                            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1705, "\"", 1745, 3);
            WriteAttributeValue("", 1715, "excluir(\'", 1715, 9, true);
#nullable restore
#line 43 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
WriteAttributeValue("", 1724, customer.IdCliente, 1724, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1743, "\')", 1743, 2, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger\" data-toggle=\"tooltip\" title=\"Excluir\">\n                                <i class=\"fa-solid fa-trash\"></i>\n                            </button>\n                        </td>\n                    </tr>\n");
#nullable restore
#line 48 "/Users/felipebotelho/Projects/Hvl-Front/Views/Customer/index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\n        </table>\n    </div>\n</div>\n\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"
<script type=""text/javascript"" charset=""utf8"" src=""https://cdn.datatables.net/1.11.5/js/jquery.dataTables.js""></script>
<script src=""https://unpkg.com/sweetalert/dist/sweetalert.min.js""></script>
    <script type=""text/javascript"">
        $(document).ready( function () {
            $('#dataTable').DataTable({
                ""language"": {
                    ""lengthMenu"": ""Exibir _MENU_ por página"",
                    ""zeroRecords"": ""Não existem registros"",
                    ""info"": ""página _PAGE_ de _PAGES_"",
                    ""paginate"": {
                        ""first"": ""Primeira"",
                        ""last"": ""Última"",
                        ""next"": ""Próxima"",
                        ""previous"": ""Anterior""
                    },
                    ""search"": ""Buscar por"",
                    ""buttons"": {
                        ""pageLength"": {
                            _: ""Exibir %d por página"",
                            '-1': ""Tout afficher""
                        }
                    ");
                WriteLiteral(@"}
                },
                dom: 'Bfrtip',
            });

            $(function () {
                $('[data-toggle=""tooltip""]').tooltip()
            })
        });

        function excluir(idCliente)
        {
  
            swal({
                    title: ""Você tem certeza?"",
                    text: ""Uma vez que remover, você nunca mais vai ver este registro!"",
                    icon: ""warning"",
                    buttons: true,
                    dangerMode: true,
                })
                .then((willDelete) => {
                if (willDelete) {
                    $.ajax({
                        url: ""/Customer/RemoveCliente?idCliente="" + idCliente,
                        type: ""GET"",
                        contentType: ""application/json"",
                        success: function(response)
                        {
                            let iconType = ""success"";
                            if(response.complete == false)
                            {
             ");
                WriteLiteral(@"                   iconType = ""warning""
                            }
                            swal(response.message, {
                                icon: iconType,
                            });

                            setTimeout(() => {
                                window.location.reload();
                            }, 3000)
                        },
                        erro: function()
                        {
                            swal(""Algo deu errado!"", {
                                icon: ""warning"",
                            });
                        }
                    })
                   
                } else {
                    swal(""Tudo bem, nada foi feito!"");
                }
            });
        }
    </script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CustomerViewModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
