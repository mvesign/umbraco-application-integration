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