#pragma checksum "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "f9612a7f0043f3451e400176becdb399b27ecd849884791111c2d47f326f8bed"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AdminPanel_Index), @"mvc.1.0.view", @"/Views/AdminPanel/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\_ViewImports.cshtml"
using SmartCafe;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\_ViewImports.cshtml"
using SmartCafe.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"f9612a7f0043f3451e400176becdb399b27ecd849884791111c2d47f326f8bed", @"/Views/AdminPanel/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"d9d9e963d07aa7fba51f19cfb2bf624e5e179161bd9feccc74a23eecfa56522a", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_AdminPanel_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<SmartCafe.Models.Drink>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/images/lagoonLogoWhiteTransparent.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("Logo"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    /* Stilovi za header */
    body {
        font-family: 'Montserrat', sans-serif;
        margin: 0;
        padding: 0;
        background-image: url('/images/abstractBackground2.png');
        color: black;
    }

    .header {
        background-color: transparent;
        color: white;
        padding: 0px 10px;
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-right: 20px; 
    }

    .nav {
        list-style-type: none !important;
        display: flex;
        align-items: center;
    }

        .nav li {
            margin-right: 35px;
            margin-top: -50px;
            text-transform: uppercase;
            font-size: 14px;
            list-style-type: none;
        }

        .nav a {
            color: white;
            text-decoration: none;
            padding-bottom: 3px;
            border-bottom: 2px solid transparent;
            transition: border-bottom-color 0.3s;
      ");
            WriteLiteral(@"      list-style-type: none;
        }

            .nav a:hover {
                border-bottom-color: #fff;
                list-style-type: none;
            }

    .nav-item {
        list-style-type: none;
    }

    .logo img {
        max-width: 35%;
        margin-right: 0px;
        margin-left: 15px;
        margin-top: -20px;
    }

    /* Stilovi za statistiku */
    .statistics {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        grid-gap: 20px;
        margin-top: 20px;
        margin-left: 20%;
        margin-right: 20%;
    }

    .statistics-item {
        display: flex;
        flex-direction: column;
        align-items: center;
        text-align: center;
        background-color: rgba(0, 0, 0, 0.5);
        padding: 20px;
        border-radius: 5px;
        color: white;
        transition: background-color 0.3s;
    }

        .statistics-item:hover {
            background-color: rgba(0, 0, 0, 0.7);
        }

    .");
            WriteLiteral(@"statistics-label {
        font-size: 18px;
        font-weight: bold;
        margin-bottom: 10px;
    }

    .statistics-value {
        font-size: 24px;
        font-weight: bold;
        margin-bottom: 10px;
    }

    .statistics-description {
        grid-column: span 2;
        text-align: center;
        margin-top: 40px;
        color: white;
    }

        .statistics-description h2 {
            font-size: 32px;
            margin-bottom: 10px;
        }

        .statistics-description p {
            font-size: 16px;
        }


    /* Stilovi za Most Sold Cocktail */
    .most-sold-cocktail {
        margin: 5% auto;
        display: flex;
        flex-direction: column;
        align-items: center;
        text-align: center;
        background-color: rgba(0, 0, 0, 0.5); /* Polu transparentna pozadina */
        padding: 20px;
        border-radius: 5px;
        color: white;
        width: fit-content;
        max-width: 20%;
    }

        .most-sol");
            WriteLiteral(@"d-cocktail img {
            max-width: 50%;
            height: auto; 
            margin-bottom: 10px;
            border-radius: 20px;
        }

        .most-sold-cocktail:hover {
            background-color: rgba(0, 0, 0, 0.7);
        }

    .most-sold-cocktail-name {
        font-size: 24px;
        margin-bottom: 10px;
    }

    /* Stilovi za carousel */
    .carousel {
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        grid-gap: 40px; 
        margin: 15%;
        margin-top: 0;
        margin-bottom: 40px;
    }

    .carousel-item {
        display: flex;
        width: fit-content;
        flex-direction: column;
        align-items: center;
        text-align: center;
        background-color: #f5f5f5;
        padding: 10px;
        margin: 2% auto;
        border-radius: 5px;
        transition: background-color 0.3s;
        cursor: pointer;
        text-decoration: none; 
        color: black; 
    }

        .carousel-ite");
            WriteLiteral(@"m:hover {
            background-color: #e0e0e0;
        }

    .carousel-image {
        width: 200px;
        max-width: 100%;
        height: 200px; 
        object-fit: cover;
        border-radius: 5px;
    }

    /* Stilovi za modalni prozor */
    .modal {
        display: none; /* Inicijalno sakriven */
        position: fixed;
        z-index: 1;
        left: 0;
        top: 0;
        width: 100%;
        height: 100%;
        overflow: auto;
        background-color: rgba(0, 0, 0, 0.4); /* Polu transparentna pozadina */
    }

    .modal-content {
        background-color: #fefefe;
        margin: 15% auto;
        padding: 20px;
        border: 1px solid #888;
        width: 80%;
        max-width: 600px;
        border-radius: 5px;
        position: relative;
        display: flex; 
    }

    .edit-image {
        max-width: 40%;
        height: auto;
        border-radius: 10px;
        margin-right: 20px; 
    }

    .edit-details {
        display");
            WriteLiteral(@": flex;
        flex-direction: column;
        justify-content: center;
    }

    .close {
        color: #aaa;
        position: absolute;
        top: 10px;
        right: 20px;
        font-size: 28px;
        font-weight: bold;
        cursor: pointer;
    }

        .close:hover,
        .close:focus {
            color: black;
            text-decoration: none;
            cursor: pointer;
        }

    .modal h1 {
        margin-top: 0;
    }

    .modal form {
        margin-top: 20px;
    }

    .form-group {
        margin-bottom: 20px;
    }

        .form-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .form-group input[type=""text""],
        .form-group input[type=""number""] {
            width: 100%;
            padding: 5px;
            font-size: 16px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        .form-group input[type=""submit""]");
            WriteLiteral(@" {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            font-size: 16px;
            border-radius: 5px;
            border: none;
            cursor: pointer;
        }

            .form-group input[type=""submit""]:hover {
                background-color: #45a049;
            }

    .logout-button {
        display: inline-block;
        position: relative;
        z-index: 1;
        overflow: hidden;
        text-decoration: none;
        font-family: sans-serif;
        font-weight: 600;
        font-size: 2em;
        padding: 0.75em 1em;
        color: #FF5A5F;
        border: 0.15em solid #FF5A5F;
        border-radius: calc(0.75em + 0.5em + 0.15em);
        transition: 3s;
    }

        .logout-button:before,
        .logout-button:after {
            content: '';
            position: absolute;
            top: -1.5em;
            z-index: -1;
            width: 200%;
            aspect-ratio: 1;
         ");
            WriteLiteral(@"   border: none;
            border-radius: 40%;
            background-color: rgba(0, 0, 0, 0.7);
            transition: 4s;
        }

        .logout-button:before {
            left: -80%;
            transform: translate3d(0, 5em, 0) rotate(-340deg);
        }

        .logout-button:after {
            right: -80%;
            transform: translate3d(0, 5em, 0) rotate(390deg);
        }

        .logout-button:hover,
        .logout-button:focus {
            color: #FFFFFF;
        }

            .logout-button:hover:before,
            .logout-button:hover:after,
            .logout-button:focus:before,
            .logout-button:focus:after {
                transform: none;
                background-color: rgba(255, 90, 95, 0.75); /* Življa boja */
            }

    /* Team Members */
    .team-grid {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        grid-template-rows: repeat(2, 1fr);
        gap: 10px;
        justify-content: center");
            WriteLiteral(@";
        align-items: center;
        margin-bottom: 20px; 
    }

    .team-member {
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        width: 150px;
        margin: 0 auto; 
        box-sizing: border-box; 
        padding: 10px; 
    }

    .team-heading {
        font-size: 24px;
        font-weight: bold;
        margin-bottom: 10px;
        color: white;
        text-align: center;
        margin-top: 0;
    }

    .team-member {
        display: flex;
        flex-direction: column;
        align-items: center;
        text-align: center;
        width: 200px;
    }

        .team-member img {
            width: 150px;
            height: 150px;
            object-fit: cover;
            border-radius: 50%;
            border: 3px solid #fff;
            box-shadow: 0px 2px 6px rgba(0, 0, 0, 0.4);
            transition: all 0.3s ease-in-out;
        }

            .team-member img:hove");
            WriteLiteral(@"r {
                transform: scale(1.1);
            }

    .member-job {
        font-size: 12px;
        margin-top: 2px;
        color: #ffcc33; /* Tamnije žuta boja */
    }

    .member-name {
        text-transform: uppercase;
        font-weight: bold;
        color: #fff; /* Bijela boja za ime i prezime */
        margin-top: 5px;
    }

    .search-container {
        height: 100vh; 
        font-family: 'Montserrat', sans-serif;
    }

    #searchInputIngredient, #searchInputName{
        display: block;
        margin: 3% auto 2% auto;
        padding: 10px;
        border-radius: 5px;
        border: 1px solid #ccc;
        width: 500px;
        font-family: 'Montserrat', sans-serif;
    }
</style>

<!DOCTYPE html>
<html>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9612a7f0043f3451e400176becdb399b27ecd849884791111c2d47f326f8bed15885", async() => {
                WriteLiteral("\r\n    <title>Lagoon\'s Cocktail Bar - Admin Panel</title>\r\n    <link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css\">\r\n");
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
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9612a7f0043f3451e400176becdb399b27ecd849884791111c2d47f326f8bed17052", async() => {
                WriteLiteral("\r\n    <div class=\"header\">\r\n        <div class=\"logo\">\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "f9612a7f0043f3451e400176becdb399b27ecd849884791111c2d47f326f8bed17409", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
        </div>
        <ul class=""navbar-nav"">
            <h1 style=""margin-right:2px;"">Admin Panel</h1>
        </ul>
    </div>

    <div class=""statistics"">
        <div class=""statistics-description"">
            <h2>Lagoon Bar Stats</h2>
            <p>Welcome to the Lagoon Bar Stats section, where you can check out some key metrics related to the performance of our bar.</p>
        </div>
        <div class=""statistics-item"">
            <span class=""statistics-label"">Number of workers</span>
            <span class=""statistics-value"" id=""numWorkersValue"">6</span>
        </div>
        <div class=""statistics-item"">
            <span class=""statistics-label"">Number of customers</span>
            <span class=""statistics-value"" id=""numCustomersValue"">4185</span>
        </div>
    </div>
    <ul>
");
#nullable restore
#line 456 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
         foreach (var ingredient in ViewBag.SortedIngredients)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <li>");
#nullable restore
#line 458 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
           Write(ingredient.name);

#line default
#line hidden
#nullable disable
                WriteLiteral(" - ");
#nullable restore
#line 458 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
                              Write(ingredient.quantity);

#line default
#line hidden
#nullable disable
                WriteLiteral("</li>\r\n");
#nullable restore
#line 459 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"    </ul>

    <script>
        function animateValue(element, start, end, duration) {
          const range = end - start;
          const increment = end > start ? 1 : -1;
          const stepTime = Math.abs(Math.floor(duration / range));
          const obj = element;

          let current = start;

          const timer = setInterval(() => {
            current += increment;
            obj.textContent = current;
            if ((increment > 0 && current >= end) || (increment < 0 && current <= end)) {
              clearInterval(timer);
              obj.textContent = end;
            }
          }, stepTime);
        }

        const numWorkersValue = document.getElementById('numWorkersValue');
        animateValue(numWorkersValue, 0, 6, 2000); // Trajanje animacije je 2000ms (2 sekunde)

        const numCustomersValue = document.getElementById('numCustomersValue');
        animateValue(numCustomersValue, 0, 4185, 2000); // Trajanje animacije je 2000ms (2 sekunde)

    </scri");
                WriteLiteral(@"pt>

    <div>
        <input type=""text"" id=""searchInputName"" onkeyup=""filterDrinksByName()"" placeholder=""Search for drinks by name.."">
    </div>

    <script>
        function filterDrinksByName() {
            var input, filter, carousel, drinkItems, name, i, txtValue;
            input = document.getElementById('searchInputName');
            filter = input.value.toUpperCase();
            carousel = document.getElementsByClassName('carousel')[0];
            drinkItems = carousel.getElementsByClassName('carousel-item');
            for (i = 0; i < drinkItems.length; i++) {
                name = drinkItems[i].getElementsByTagName('h3')[0];
                txtValue = name.textContent || name.innerText;
                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                    drinkItems[i].style.display = '';
                } else {
                    drinkItems[i].style.display = 'none';
                }
            }
        }
    </script>

    <div>
        <i");
                WriteLiteral(@"nput type=""text"" id=""searchInputIngredient"" onkeyup=""filterDrinksByIngredients()"" placeholder=""Search for drinks by ingredients.."">
    </div>

    <ul id=""searchResults""></ul>

    <script>
        function filterDrinksByIngredients() {
            var input, filter, carousel, drinkItems, url;
            input = document.getElementById('searchInputIngredient');
            filter = input.value.toUpperCase();
            carousel = document.getElementsByClassName('carousel')[0];
            drinkItems = carousel.getElementsByClassName('carousel-item');

            var url = '");
#nullable restore
#line 526 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
                  Write(Url.Action("SearchDrinksByIngredient", "AdminPanel"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"';
            fetch(`${url}?searchTerm=${filter}`)
                .then(response => response.json())
                .then(data => {
                    // Clear previous search results
                    document.getElementById('searchResults').innerHTML = '';

                    // Populate the search results
                    var ul = document.getElementById('searchResults');
                    data.forEach(item => {
                        var li = document.createElement('li');
                        li.appendChild(document.createTextNode(item.name));
                        ul.appendChild(li);
                    });

                    Array.from(drinkItems).forEach(item => {
                        var name = item.getElementsByTagName('h3')[0];
                        var txtValue = name.textContent || name.innerText;
                        if (!txtValue.toUpperCase().includes(filter)) {
                            item.style.display = 'none';
                        } els");
                WriteLiteral("e {\r\n                            item.style.display = \'\';\r\n                        }\r\n                    });\r\n                })\r\n                .catch(error => console.error(\'Error:\', error));\r\n        }\r\n    </script>\r\n\r\n    <div class=\"carousel\">\r\n");
#nullable restore
#line 556 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
         foreach (var drink in Model)
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div class=\"carousel-item\">\r\n                <h3>");
#nullable restore
#line 559 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
               Write(drink.name);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n                <p>Price: ");
#nullable restore
#line 560 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
                     Write(drink.price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n                <img");
                BeginWriteAttribute("src", " src=\"", 15713, "\"", 15766, 1);
#nullable restore
#line 561 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 15719, Url.Content("~/images/" + drink.name + ".jpg"), 15719, 47, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("alt", " alt=\"", 15767, "\"", 15784, 1);
#nullable restore
#line 561 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 15773, drink.name, 15773, 11, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"carousel-image\" />\r\n\r\n                <!-- Modalni prozor za uređivanje -->\r\n                <div");
                BeginWriteAttribute("id", " id=\"", 15890, "\"", 15910, 2);
                WriteAttributeValue("", 15895, "modal-", 15895, 6, true);
#nullable restore
#line 564 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 15901, drink.id, 15901, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" class=\"modal\">\r\n                    <div class=\"modal-content\">\r\n                        \r\n                        <h1>Edit Drink</h1>\r\n                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f9612a7f0043f3451e400176becdb399b27ecd849884791111c2d47f326f8bed27061", async() => {
                    WriteLiteral("\r\n                            <img");
                    BeginWriteAttribute("src", " src=\"", 16169, "\"", 16222, 1);
#nullable restore
#line 569 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 16175, Url.Content("~/images/" + drink.name + ".jpg"), 16175, 47, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    BeginWriteAttribute("alt", " alt=\"", 16223, "\"", 16240, 1);
#nullable restore
#line 569 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 16229, drink.name, 16229, 11, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" class=\"edit-image\" />\r\n                            <div class=\"form-group\">\r\n                                <label for=\"name\">Name:</label>\r\n                                <input type=\"text\" id=\"name\" name=\"name\"");
                    BeginWriteAttribute("value", " value=\"", 16456, "\"", 16475, 1);
#nullable restore
#line 572 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 16464, drink.name, 16464, 11, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                            </div>\r\n                            <div class=\"form-group\">\r\n                                <label for=\"price\">Price:</label>\r\n                                <input type=\"number\" id=\"price\" name=\"price\"");
                    BeginWriteAttribute("value", " value=\"", 16714, "\"", 16734, 1);
#nullable restore
#line 576 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 16722, drink.price, 16722, 12, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(" />\r\n                            </div>\r\n\r\n\r\n                            <div class=\"form-group\">\r\n                                <input type=\"submit\" value=\"Save\" />\r\n                            </div>\r\n                        ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 568 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
                                                                WriteLiteral(drink.id);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 587 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("    </div>\r\n    <br />\r\n    <h3>");
#nullable restore
#line 590 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
   Write(ViewBag.OptimalProfit);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h3>\r\n    <br />\r\n    <div class=\"team-heading\">Team Members</div>\r\n    <div class=\"team-grid\">\r\n    <div class=\"team-member\">\r\n        <img");
                BeginWriteAttribute("src", " src=\"", 17251, "\"", 17297, 1);
#nullable restore
#line 595 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 17257, Url.Content("~/images/teamMember1.jpg"), 17257, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Uposlenik 1\" />\r\n        <div class=\"member-name\">Hanna Barker</div>\r\n        <div class=\"member-job\">HR Manager</div>\r\n    </div>\r\n    <div class=\"team-member\">\r\n        <img");
                BeginWriteAttribute("src", " src=\"", 17479, "\"", 17525, 1);
#nullable restore
#line 600 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 17485, Url.Content("~/images/teamMember2.jpg"), 17485, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Uposlenik 2\" />\r\n        <div class=\"member-name\">Sam Miller</div>\r\n        <div class=\"member-job\">Bartender</div>\r\n    </div>\r\n    <div class=\"team-member\">\r\n        <img");
                BeginWriteAttribute("src", " src=\"", 17704, "\"", 17750, 1);
#nullable restore
#line 605 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 17710, Url.Content("~/images/teamMember3.jpg"), 17710, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Uposlenik 3\" />\r\n        <div class=\"member-name\">Clayton Lane</div>\r\n        <div class=\"member-job\">Bartender</div>\r\n    </div>\r\n    <div class=\"team-member\">\r\n        <img");
                BeginWriteAttribute("src", " src=\"", 17931, "\"", 17977, 1);
#nullable restore
#line 610 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 17937, Url.Content("~/images/teamMember4.jpg"), 17937, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Uposlenik 4\" />\r\n        <div class=\"member-name\">Robert Barkfield</div>\r\n        <div class=\"member-job\">Bartender</div>\r\n    </div>\r\n    <div class=\"team-member\">\r\n        <img");
                BeginWriteAttribute("src", " src=\"", 18162, "\"", 18208, 1);
#nullable restore
#line 615 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 18168, Url.Content("~/images/teamMember5.jpg"), 18168, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" alt=\"Uposlenik 5\" />\r\n        <div class=\"member-name\">Margaret Jones</div>\r\n        <div class=\"member-job\">Bartender</div>\r\n    </div>\r\n    <div class=\"team-member\">\r\n        <img");
                BeginWriteAttribute("src", " src=\"", 18391, "\"", 18437, 1);
#nullable restore
#line 620 "C:\Users\efend\Desktop\VVS\VVS\VVSProject\Views\AdminPanel\Index.cshtml"
WriteAttributeValue("", 18397, Url.Content("~/images/teamMember6.jpg"), 18397, 40, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" alt=""Uposlenik 6"" />
        <div class=""member-name"">Jenny Francis</div>
        <div class=""member-job"">Finance Manager</div>
    </div>
    </div>

    <script>
        // JavaScript kod za modalni prozor
        var modalItems = document.getElementsByClassName(""carousel-item"");
        var modals = document.getElementsByClassName(""modal"");

        // Prikazuje modalni prozor za određenu stavku
        function showModal(itemIndex) {
            modals[itemIndex].style.display = ""block"";
        }

        // Postavlja događaje za prikazivanje/skrivanje modalnih prozora
        for (var i = 0; i < modalItems.length; i++) {
            (function (index) {
                modalItems[index].addEventListener(""click"", function () {
                    showModal(index);
                });

                var closeButtons = modals[index].getElementsByClassName(""close"");
                for (var j = 0; j < closeButtons.length; j++) {
                    closeButtons[j].addEventListener");
                WriteLiteral(@"(""click"", function () {
                        hideModal(index);
                    });
                }

                // Sakriva modalni prozor za određenu stavku
                function hideModal(itemIndex) {
                    modals[itemIndex].style.display = ""none"";
                }
            })(i);
        }
    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<IdentityUser> UserManager { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<IdentityUser> SignInManager { get; private set; } = default!;
        #nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<SmartCafe.Models.Drink>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
