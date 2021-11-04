# Umbraco v9 Demo
Demo site build in Umbraco v.9.0.0-rc003 and .NET Core 5.0.

![cover image](cover.png)

## About this solution:
This is a demo site for Umbraco v9 build in the new .NET Core v5 version. 
It is built as an experiment/investigation and should not be used as a template for your next Umbraco site.
Use it as a reference if you will and steal whatever you like and ignore the things you dont.

This is not **the way** of building Umbraco sites, it's **a way**. There are less complex ways of building Umbraco sites *(using ModelsBuilder for example, [see discussion](https://github.com/Adolfi/UmbracoNineDemoSite/issues/10))* so if you are new to Umbraco make sure you [read the official docs](https://our.umbraco.com/documentation/) and don't use this this project as your only source of inspiration.

### Content:
- [Every page is rendered through a RenderController *(Route Hijacking)*](UmbracoNineDemoSite.Core/Features/Home/HomeController.cs).
- [Every Blocklist item is rendered with block specific ViewComponents with strongly typed view models](UmbracoNineDemoSite.Web/Views/Partials/_BlockList.cshtml).
- [Every view uses strongly typed View Models *(inheriting from ContentModel)*](UmbracoNineDemoSite.Web/Views/Home.cshtml).
- [Page components that handles logic are rendered through ViewComponents](UmbracoNineDemoSite.Core/Features/Shared/Components/Header/HeaderViewComponent.cs).
- [View components have Unit Tests](UmbracoNineDemoSite.Tests/Unit/Features/Shared/Components/Footer/FooterViewComponentTests.cs).
- [Non-logic components are rendered through Partial views](UmbracoNineDemoSite.Web/Views/Partials/_SectionHeader.cshtml).
- [Custom services are registered in the IUmbracoBuilder through an IComposer](UmbracoNineDemoSite.Core/Features/Shared/Settings/SiteSettingsComposer.cs).
- [Constant classes are used when accessing content or property to avoid spreading magic strings](UmbracoNineDemoSite.Core/Features/Shared/Constants/PropertyAlias.cs).
- [Forms are submitted through a SurfaceController](UmbracoNineDemoSite.Core/Features/Shared/Components/ContactForm).
- [Examine searching. (#h5yr Andrey Karandashov)](UmbracoNineDemoSite.Core/Features/Search)
- [Products are fetched from external source and rendered using a ContentFinder](UmbracoNineDemoSite.Core/Features/Products/ProductsContentFinder.cs).

#### Unit Tests:
- [RenderController: Home](UmbracoNineDemoSite.Tests/Unit/Features/Home/HomeControllerTests.cs)
- [RenderController: Page](UmbracoNineDemoSite.Tests/Unit/Features/Page/PageControllerTests.cs)
- [ViewComponents: ContentBlock](UmbracoNineDemoSite.Tests/Unit/Features/Shared/Components/ContentBlock/ContentBlockViewComponentTests.cs)
- [ViewComponents: Header](UmbracoNineDemoSite.Tests/Unit/Features/Shared/Components/Header/HeaderViewComponentTests.cs)
- [ViewComponent: Footer](UmbracoNineDemoSite.Tests/Unit/Features/Shared/Components/Footer/FooterViewComponentTests.cs)
- [SurfaceController: ContactForm](UmbracoNineDemoSite.Tests/Unit/Features/Shared/Components/ContactForm/ContactFormControllerTests.cs)
- [UmbracoHelper: SiteSettings](UmbracoNineDemoSite.Tests/Unit/Features/Shared/Settings/SiteSettingsTests.cs)
- [Relations: NavigationService](UmbracoNineDemoSite.Tests/Unit/Features/Shared/Components/Navigation/NavigationServiceTests.cs)
- [ContentFinder: Products](UmbracoNineDemoSite.Tests/Unit/Features/Products/ProductsContentFinderTests.cs)

### Login:
This site uses an embeded SQLCE database to avoid having to restore and keep updating a restore script.
To login to the Umbraco backoffice use these credentials:
- Username: admin@admin.com
- Password: adminadmin

### Read more:
Go to [adolfi.dev](https://adolfi.dev) if you want to read more Umbraco and Unit Testing articles.
