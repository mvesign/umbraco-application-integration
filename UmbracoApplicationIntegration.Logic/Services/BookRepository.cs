using System.Linq.Expressions;
using Umbraco.Cms.Core.Models;
using Umbraco.UIBuilder;
using Umbraco.UIBuilder.Persistence;
using UmbracoApplicationIntegration.Logic.Exceptions;
using UmbracoApplicationIntegration.Models;

namespace UmbracoApplicationIntegration.Logic.Services;

public sealed class BookRepository(RepositoryContext context, BookService bookService) : Repository<Book, int>(context)
{
    #region Obsolete Methods

    [Obsolete]
    protected override void DeleteImpl(int id) => throw new NotImplementedException();

    [Obsolete]
    protected override IEnumerable<Book> GetAllImpl(Expression<Func<Book, bool>>? whereClause = null, Expression<Func<Book, object>>? orderBy = null, SortDirection orderByDirection = SortDirection.Ascending) => throw new NotImplementedException();

    [Obsolete]
    protected override long GetCountImpl(Expression<Func<Book, bool>> whereClause) => throw new NotImplementedException();

    [Obsolete]
    protected override int GetIdImpl(Book entity) => throw new NotImplementedException();

    [Obsolete]
    protected override Book GetImpl(int id) => throw new NotImplementedException();

    [Obsolete]
    protected override PagedModel<Book> GetPagedImpl(int pageNumber, int pageSize, Expression<Func<Book, bool>>? whereClause = null, Expression<Func<Book, object>>? orderBy = null, SortDirection orderByDirection = SortDirection.Ascending) => throw new NotImplementedException();

    [Obsolete]
    protected override IEnumerable<TJunctionEntity> GetRelationsByParentIdImpl<TJunctionEntity>(int parentId, string relationAlias) => throw new NotImplementedException();

    [Obsolete]
    protected override Book SaveImpl(Book entity) => throw new NotImplementedException();

    [Obsolete]
    protected override TJunctionEntity SaveRelationImpl<TJunctionEntity>(TJunctionEntity entity) => throw new NotImplementedException();

    #endregion

    protected override Task DeleteImplAsync(int id, CancellationToken cancellationToken = default)
    {
        var books = bookService.GetClassicBooks();

        var bookToDelete = books.FirstOrDefault(book => book.Id == id);
        if (bookToDelete == null)
        {
            return Task.CompletedTask;
        }

        if (books.Remove(bookToDelete))
        {
            bookService.WriteClassicBooks(books);
        }

        return Task.CompletedTask;
    }
    
    protected override Task<IEnumerable<Book>> GetAllImplAsync(
        Expression<Func<Book, bool>>? whereClause = null,
        Expression<Func<Book, object>>? orderBy = null,
        SortDirection orderByDirection = SortDirection.Ascending,
        CancellationToken cancellationToken = default)
    {
        var books = bookService.GetClassicBooks();

        if (whereClause != null)
        {
            books = [.. books.AsQueryable().Where(whereClause)];
        }

        if (orderBy != null)
        {
            books = orderByDirection == SortDirection.Ascending
                ? [.. books.AsQueryable().OrderBy(orderBy)]
                : [.. books.AsQueryable().OrderByDescending(orderBy)];
        }
        else
        {
            books = orderByDirection == SortDirection.Ascending
                ? [.. books.OrderBy(book => book.Id)]
                : [.. books.OrderByDescending(book => book.Id)];
        }

        return Task.FromResult<IEnumerable<Book>>(books);
    }
    
    protected override Task<long> GetCountImplAsync(Expression<Func<Book, bool>>? whereClause, CancellationToken cancellationToken = default)
    {
        var books = bookService.GetClassicBooks();

        var count = whereClause != null
            ? books.AsQueryable().Count(whereClause)
            : books.Count;

        return Task.FromResult<long>(count);
    }

    protected override Task<int> GetIdImplAsync(Book entity, CancellationToken cancellationToken = default) =>
        Task.FromResult(entity.Id);

    protected override Task<Book> GetImplAsync(int id, CancellationToken cancellationToken = default)
    {
        var book = bookService.GetClassicBooks().FirstOrDefault(book => book.Id == id);

        return book != null
            ? Task.FromResult(book)
            : throw new BookNotFoundException();
    }

    protected override Task<PagedModel<Book>> GetPagedImplAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<Book, bool>>? whereClause = null,
        Expression<Func<Book, object>>? orderBy = null,
        SortDirection orderByDirection = SortDirection.Ascending,
        CancellationToken cancellationToken = default)
    {
        var books = bookService.GetClassicBooks();

        if (whereClause != null)
        {
            books = [.. books.AsQueryable().Where(whereClause)];
        }

        if (orderBy != null)
        {
            books = orderByDirection == SortDirection.Ascending
                ? [.. books.AsQueryable().OrderBy(orderBy)]
                : [.. books.AsQueryable().OrderByDescending(orderBy)];
        }
        else
        {
            books = orderByDirection == SortDirection.Ascending
                ? [.. books.OrderBy(book => book.Id)]
                : [.. books.OrderByDescending(book => book.Id)];
        }

        return Task.FromResult(new PagedModel<Book>(books.Count, books));
    }

    protected override Task<IEnumerable<TJunctionEntity>> GetRelationsByParentIdImplAsync<TJunctionEntity>(
        int parentId,
        string relationAlias,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    protected override Task<Book> SaveImplAsync(Book entity, CancellationToken cancellationToken = default)
    {
        var books = bookService.GetClassicBooks();

        var existingBook = books.FirstOrDefault(book => book.Id == entity.Id);
        if (existingBook != null)
        {
            books.Remove(existingBook);
        }
        else
        {
            var highestId = books.Any() ? books.Max(book => book.Id) : 0;
            entity.Id = highestId + 1;
        }

        books.Add(entity);

        bookService.WriteClassicBooks(books);

        return Task.FromResult(entity);
    }

    protected override Task<TJunctionEntity> SaveRelationImplAsync<TJunctionEntity>(TJunctionEntity entity, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}
