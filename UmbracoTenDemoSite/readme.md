# Umbraco 10 Demo Site 

## Upgrade from 9.4.2
- Create new Umbraco 10rc5 site with sqlite db, build and run.
- Added projects as in UmbarcoNineDemo with v10rc5 dependencies.
- Copied project .cs files from UmbarcoNineDemo.
- - Build error in .Core project related to: IContentFinder 
- - - ProductsContentFinder.cs: IContentFinder.TryFindContent(IPublishedRequestBuilder request) now retuns Task<bool> instead of bool.
- - - UmbracoContentComposer.cs: UmbracoContextIndex_TransformingIndexValues uses e.ValueSet.Set(), but in Examine 3 ValueSet.Values are of type IReadOnlyDictionary and cannot be set.
- Add Modelsbuilder configuration to appsettings.json and appsettings.Development.json.
- Copy usync files from UmbracoNineDemoSite.
- - Import everthing, export everything without issues.
- - Generate Models