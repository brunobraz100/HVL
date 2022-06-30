#pragma checksum "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eab2a178181ab6ec8d113ff0cf4008913f7f7df1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Documents_Index), @"mvc.1.0.view", @"/Views/Documents/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eab2a178181ab6ec8d113ff0cf4008913f7f7df1", @"/Views/Documents/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"33012d7e4a89a70e64c4147e5337f46a90f9cb60", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Documents_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<DocumentModel>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
  
    ViewData["Title"] = "Documentos";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<link rel=""stylesheet"" type=""text/css"" href=""https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css"">

<div class=""card mb-4"">
    <br />
    <div class=""card-header"">
        <i class=""fa-solid fa-file-arrow-up""></i>
        Documentos
        <a  class=""btn btn-primary btn-floating""");
            BeginWriteAttribute("href", " href=\"", 369, "\"", 444, 1);
#nullable restore
#line 14 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
WriteAttributeValue("", 376, Url.Action("Add", "Documents", new {IdCliente = ViewBag.IdCliente}), 376, 68, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
            <i class=""fa-solid fa-file-circle-plus""></i>
            Novo Documento
        </a>
    </div>
    <div class=""card-body table-responsive"">
        <table id=""dataTable"" class=""table"">
            <thead>
                <tr>
                    <td>Nome</td>
                    <td>Categoria</td>
                    <td>Data da Carga</td>
                    <td>Data de validade</td>
                    <td>Ações</td>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 31 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
                 foreach (DocumentModel document in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td>");
#nullable restore
#line 34 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
                       Write(document.DsDocumento);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>");
#nullable restore
#line 35 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
                       Write(document.DsCategoria);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>");
#nullable restore
#line 36 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
                        Write(document.DtCarga.HasValue ? document.DtCarga.Value.ToString("dd/MM/yyyy") : "");

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\n                        <td>");
#nullable restore
#line 37 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
                       Write(document.DtValidade.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\n                        <td>\n                            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1416, "\"", 1458, 3);
            WriteAttributeValue("", 1426, "excluir(\'", 1426, 9, true);
#nullable restore
#line 39 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
WriteAttributeValue("", 1435, document.IdDocumento, 1435, 21, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1456, "\')", 1456, 2, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger\" data-toggle=\"tooltip\" title=\"Excluir\">\n                                <i class=\"fa-solid fa-trash\"></i>\n                            </button>\n                        </td>\n                    </tr>\n");
#nullable restore
#line 44 "/Users/felipebotelho/Projects/Hvl-Front/Views/Documents/Index.cshtml"
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

        function excluir(idDocumento)
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
                        url: ""/Documents/DeleteDocument?IdDocument="" + idDocumento,
                        type: ""GET"",
                        contentType: ""application/json"",
                        success: function(response)
                        {
                            let iconType = ""success"";
                            if(response.complete == false)
                            {
      ");
                WriteLiteral(@"                          iconType = ""warning""
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<DocumentModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591