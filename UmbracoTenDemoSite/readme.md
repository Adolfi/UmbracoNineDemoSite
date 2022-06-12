# Umbraco 10 Demo Site 

## Upgrade from 9.4.2
- Create new Umbraco 10rc5 site with sqlite db, build and run.
- Added projects as in UmbracoNineDemoSite with v10rc5 dependencies.
- Copied project .cs files from UmbracoNineDemoSite.
- - Build error in .Core project related to: IContentFinder 
- - - ProductsContentFinder.cs: IContentFinder.TryFindContent(IPublishedRequestBuilder request) now retuns Task<bool> instead of bool.
- - - UmbracoContentComposer.cs: UmbracoContextIndex_TransformingIndexValues uses e.ValueSet.Set(), but in Examine 3 ValueSet.Values are of type IReadOnlyDictionary and cannot be set.
- Add Modelsbuilder configuration to appsettings.json and appsettings.Development.json.
- Copy usync files from UmbracoTenDemoSite.
- - Fix namespaces (replace UmbracoNineDemoSite by UmbracoTenDemoSite)
- - Import everthing, export everything without issues.
- - Generate Models
- - Add .Core project reference to .Web project.
- - - Fix missing templates (usync does not import them correctly?). Copy wwwroot (without Umbraco folder) and view contents from UmbracoNineDemoSite.
- add and fix .Test project.
- - ProductsContentFinderTests.cs IContentFinder.TryFindContent is async in v10.