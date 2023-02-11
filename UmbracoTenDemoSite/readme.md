# Umbraco 10 Demo Site 

## Upgrade from 9.4.2 to 10.0.0-rc5
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

## Upgrade from 10.0.0-rc5 to 10.0.0
Just update NuGet packages in the following order:
1. Umbraco.Cms.Web.Website
2. Umbraco.Cms
3. uSync.Core
4. uSync

## Upgrade from 10.0.0 to 10.4.0
- Same as Upgrade from 10.0.0-rc5 to 10.0.0, but unistall uSync and uSync.Core before installing uSync version 10.3.2
- Export all in uSync
- update other dependencies: Microsoft.NET.Test.Sdk => 17.4.1, Moq => 4.18.4, NUnit3TestAdapter => 4.3.1
- ModelsBuilder generate models

## Upgrade from 10.4.0 to 11.1.0
- Open properties of all four projects and set target framework to .Net 7.0
- Update Umbraco and uSynv to version 11.1.0

Run website, goto back office, select uSync in Settings section and import Content.
You may have to rebuild the Examine indexes.