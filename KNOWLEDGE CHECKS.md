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