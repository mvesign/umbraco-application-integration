# Umbraco Application Integration

Course contains several knowledge checks exercises. Below are the questions presented in these knowledge checks.

## Exercise 1: 

1. *What is the primary purpose of the Umbraco Mapper?*
   - To map properties between different object types.

2. *Which of the following is NOT a feature of the Umbraco Mapper?*
   - Creating a new database for external data storage.
     Features which are part of the Umbraco Mapper are:
     - Explicit, hand-written mapping definitions for clear transformations.
     - Built-in support for mapping only Umbraco content types.
     - Automatic generation of mapping profiles at runtime.

3. *What is required to define a mapping definition in the Umbraco Mapper?*
   - Implementing the `IMapDefinition` interface.

4. *Which Umbraco Mapper method would you use to map a collection of Dog objects to Cat view models?*
   - `MapEnumerable`

5. *Could this be a valid mapping operation?*
   ```csharp
   mapper.Map(umbracoDocument, target)
     .Map(umbracoDocument2, target)
     .Map(jsonFile, target.PropertyName);
   ```
   - True. This demonstrates chaining multiple mapping operations and using multiple source objects to construct a single target object

## Exercise 2:

1. *To edit custom data in your Umbraco project you need to use the Umbraco UI Builder.*
   - False. You need custom extensions. These can be created with or without using the UI Builder.

2. *Which storage method is best suited for smaller, frequently changing external datasets?*
   - Repository.
     Repositories are ideal for smaller, frequently changing datasets because they provide flexibility in data handling and allow developers to easily adjust the model structure without requiring extensive setup. This makes repositories more suitable for dynamic datasets than a custom database table, which is better for static, large datasets.

3. *When is using a repository generally NOT recommended?*
   - For storing static datasets that require CRUD operations.
     Repositories are not ideal for static datasets requiring CRUD operations because such datasets benefit more from being stored in a custom database table. The database approach is better suited for handling large, static datasets with structured CRUD needs, offering efficiency and durability.

4. *Why does the `BookRepository` inherit from the `Repository` class (i.e., `BookRepository : Repository<Book, int>`)?*
   - To automatically provide basic CRUD operations and functionality for managing Book entities.
     The Repository class provides an abstract layer for managing entities, including methods like SaveImpl, GetImpl, and DeleteImpl, which are inherited by BookRepository.

5. *Drag the methods to create a Cats section with a collection of Cat objects in the tree*
   ```csharp
   public static class Example
   {
       public static void AddCatsSection(this UIBuilderConfigBuilder builder)
       {
           builder.AddSection("Cats", sectionConfig =>
           {
               sectionConfig.Tree(treeConfig =>
               {
                   treeConfig.AddCollection<Cat>(x => x.id, "Cat", "Cats", "A collection of cats", collectionConfig =>
                   {
                       collectionConfig.SetAlias("cats");
                   });
               });
           });
       }
   }
   ```

## Exercise 4:

1. *Why might Algolia be a better option for your search feature than Examine? (pick multiple)*
   - Algolia is easier to scale for high-traffic applications.
   - Algolia supports dynamic updates to search indexes without manual intervention.
   - Algolia includes built-in analytics to monitor and optimize search performance.

2. *What does the Umbraco.Cms.Integrations.Search.Algolia package provide?*
   - Automation tools for indexing Umbraco content in Algolia

3. *How many search requests per month does Algolia's free tier currently support?*
   - 10,000 requests.

4. *Which JavaScript library lets you integrate Algolia’s search UI on the frontend?*
   - InstantSearch

## Exercise 5:

1. *Which file format is NOT supported when uploading data to an Algolia index?*
   - XML.

2. *How many records can you store in your Algolia index on a free plan?*
   - 1000000.

3. *Algolia has an intuitive user interface for managing search configurations.*
   - The answer is `True`, but this is an opinion of Umbraco. Weird to have this as an actual exercise question.

4. *The request response time depends on a number of things, e.g. the query complexity, data center proximity and network conditions. How long is the typical request response time for an Algolia index?*
   - <20 ms.

## Exercise 6:

1. *Arrange the steps in the recommended order to create your custom Examine index.*
   1. `{Name}IndexConstants`: Create custom index model of type `T`.
   2. `{Name}IndexValueSetBuilder`: Create custom index builder of type `IValueSetBuilder<T>`.
   3. `{Name}IndexPopulator`: Create custom index populator of type `IndexPopulator`.
   4. `Configure{Name}IndexOptions`: Create custom index named options of type `IConfigureNamedOptions<LuceneDirectoryIndexOptions>`.
   5. `{Name}LuceneIndex`: Create custom index of type `UmbracoExamineIndex`.
   6. Register index classes with a composer.

2. *What are the classes for?*
   - `{Name}IndexConstants`
     Defines the structure of the data to be indexed.
   - `{Name}IndexValueSetBuilder`
     Transforms incoming data into ValueSet objects - the format required for indexing.
   - `{Name}IndexPopulator`
     Retrieves data from an external source and populates the custom index by passing this data through the ValueSetBuilder.
   - `Configure{Name}IndexOptions`
     Configures the fields, analyzers, and types in the custom index.
   - `{Name}LuceneIndex`
     Implements a custom version of the Examine index, allowing unique configurations or extensions to the indexing process.

3. *Which method?*
   - If we were fetching the IT Book data from somewhere else we could use the exact same approach to get the data into a custom Examine index.
     The only method we would need to change would be the method where we `populate` the indexes.

## Exercise 7:

1. *What does HubSpot offer? (pick multiple)*
   - Integration with CRM for streamlined marketing, sales, and customer service.
   - Tools for creating content, managing leads, and analyzing performance.
   - Features for improving customer experiences and running effective marketing campaigns.

2. *If no ApiKey field is configured in the HubSpot settings, what will be used for authentication by default?*
   - OAuth

3. *Which helper method can be used to render a HubSpot form in an Umbraco template?*
   - `@Html.RenderHubspotForm()`

4. *What happens if a private app lacks the forms scope when accessing forms via API?*
   - A `403 Forbidden` error occurs.


## Exercise 8:

1. *What is the main purpose of replacing the helper method with a custom implementation?*
   - To control form rendering and handle submitted data manually 

2. *What is the purpose of the IContentService in the custom implementation?*
   - To interact with the Umbraco database and create content using the submitted form data