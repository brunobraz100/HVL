#pragma checksum "/Users/felipebotelho/Projects/Hvl-Front/Views/Category/Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d08fbf22a8efa39c5a529a6646fb5014b4948ab0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Category_Index), @"mvc.1.0.view", @"/Views/Category/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d08fbf22a8efa39c5a529a6646fb5014b4948ab0", @"/Views/Category/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"33012d7e4a89a70e64c4147e5337f46a90f9cb60", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Category_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CategoryModel>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "/Users/felipebotelho/Projects/Hvl-Front/Views/Category/Index.cshtml"
  
    ViewData["Title"] = "Categorias";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<link rel=""stylesheet"" type=""text/css"" href=""https://cdn.datatables.net/1.11.5/css/jquery.dataTables.css"">

<div class=""card mb-4"">
    <br />
    <div class=""card-header"">
        <i class=""fa-solid fa-network-wired""></i>
        Categorias
        <a  class=""btn btn-primary btn-floating""");
            BeginWriteAttribute("href", " href=\"", 369, "\"", 406, 1);
#nullable restore
#line 14 "/Users/felipebotelho/Projects/Hvl-Front/Views/Category/Index.cshtml"
WriteAttributeValue("", 376, Url.Action("Add", "Category"), 376, 30, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
            <i class=""fa-solid fa-plus""></i>
            Nova Categoria
        </a>
    </div>
    <div class=""card-body table-responsive"">
        <table id=""dataTable"" class=""table"">
            <thead>
                <tr>
                    <td>Nome</td>
                    <td>Ações</td>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 28 "/Users/felipebotelho/Projects/Hvl-Front/Views/Category/Index.cshtml"
                 foreach (CategoryModel category in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\n                        <td>");
#nullable restore
#line 31 "/Users/felipebotelho/Projects/Hvl-Front/Views/Category/Index.cshtml"
                       Write(category.DsCategoria);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                        <td>\n                            <a");
            BeginWriteAttribute("href", " href=\"", 984, "\"", 1064, 1);
#nullable restore
#line 33 "/Users/felipebotelho/Projects/Hvl-Front/Views/Category/Index.cshtml"
WriteAttributeValue("", 991, Url.Action("Edit", "Category", new {IdCategoria = category.IdCategoria}), 991, 73, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-primary\" data-toggle=\"tooltip\" title=\"Editar\">\n                                <i class=\"fa-solid fa-pen\"></i>\n                            </a>\n                            <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1260, "\"", 1302, 3);
            WriteAttributeValue("", 1270, "excluir(\'", 1270, 9, true);
#nullable restore
#line 36 "/Users/felipebotelho/Projects/Hvl-Front/Views/Category/Index.cshtml"
WriteAttributeValue("", 1279, category.IdCategoria, 1279, 21, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1300, "\')", 1300, 2, true);
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger\" data-toggle=\"tooltip\" title=\"Excluir\">\n                                <i class=\"fa-solid fa-trash\"></i>\n                            </button>\n                        </td>\n                    </tr>\n");
#nullable restore
#line 41 "/Users/felipebotelho/Projects/Hvl-Front/Views/Category/Index.cshtml"
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

        function excluir(IdCategoria)
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
                        url: ""/Category/DeleteCategory?IdCategoria="" + IdCategoria,
                        type: ""GET"",
                        contentType: ""application/json"",
                        success: function(response)
                        {
                            let iconType = ""success"";
                            if(response.complete == false)
                            {
         ");
                WriteLiteral(@"                       iconType = ""warning""
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CategoryModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
