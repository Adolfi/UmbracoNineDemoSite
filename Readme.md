# Umbraco v9 Demo
Demo site build in Umbraco v.9.0.0-beta004 and .NET Core 5.0.

## About this solution:
This is a demo site for Umbraco v9 build in the new .NET Core v5 version. 
It is built as an experiment/investigation and should not be used as a template for your next Umbraco site.
Use it as a reference if you will and steal whatever you like and ignore the things you dont.

This is not **the way** of building Umbraco sites, it's **a way**. 
This site is built in a way that I like to build my Umbraco sites, which is a very "backend way". 
There are simpler ways of building Umbraco sites *(using ModelsBuilder for example)*, so if you are new to Umbraco make sure you [read the official docs](https://our.umbraco.com/documentation/) for more info.

### Content:
- Every page is rendered through a RenderController.
- **TODO v2**: Every block *(Blocklist vs. Nested Content?)* is rendered through a ViewComponent.
- Every view uses strongly typed View Models *(inheriting from ContentModel)*.
- Page components that handles logic are rendered through ViewComponents.
- Non-logic components are rendered through Partial views.
- Custom services are registered in the IUmbracoBuilder through an IUserComposer.
- **TODO v2**: Forms are submitted through a SurfaceController.
- **TODO v4**: Searching is submitted and handled through a controller using Examine.
- **TODO v3**: Unit Testing all the things!

### Login:
This site uses an embeded SQLCE database to avoid having to restore and keep updating a restore script.
To login to the Umbraco backoffice use these credentials:
- Username: admin@admin.com
- Password: adminadmin

### Read more:
Go to [adolfi.dev](https://adolfi.dev) if you want to read more Umbraco and Unit Testing articles.
