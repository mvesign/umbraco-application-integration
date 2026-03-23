// Setup Algolia InstantSearch.
const { applicationId, searchApiKey } = window.algoliaSettings;
const searchClient = algoliasearch(applicationId, searchApiKey);

// Setup logic for the classic books index within Algolia InstantSearch.
const bookSearch = instantsearch({
    indexName: 'BooksIndex',
    searchClient,
    searchFunction(helper) {
        const query = helper.state.query;

        if (query && query.length >= 3) {
            helper.search();
        }
        else {
            document.querySelector('#book-hits').innerHTML = '';
        }
    },
});
bookSearch.addWidget(
    instantsearch.widgets.searchBox({
        container: '#search-box-books',
        placeholder: 'Search for books...',
    })
);
bookSearch.addWidget(
    instantsearch.widgets.hits({
        container: '#book-hits',
        templates: {
            item: (hit) => `
<div>
  <h2><a href="${hit.url}">${hit.data.title}</a></h2>
  <p>by ${hit.data.author}</p>
</div>`,
        },
    })
);
bookSearch.start();

// Setup logic for the IT books index within Algolia InstantSearch.
const itbookSearch = instantsearch({
    indexName: 'ITBooksIndex',
    searchClient,
    searchFunction(helper) {
        const query = helper.state.query;

        if (query && query.length >= 3) {
            helper.search();
        }
        else {
            document.querySelector('#itbook-hits').innerHTML = '';
        }
    },
});
itbookSearch.addWidget(
    instantsearch.widgets.searchBox({
        container: '#search-box-itbooks',
        placeholder: 'Search for IT Books...',
    })
);
itbookSearch.addWidget(
    instantsearch.widgets.hits({
        container: '#itbook-hits',
        templates: {
            item: (hit) => `
<div>
    <div>
        <img src="${hit.thumbnailUrl || '/media/luffz20o/placeholder.png'}" alt="${hit.title}" style="width: 100px; height: auto;" />
    </div>
    <div>
        <strong>${hit.title}</strong><br>
        <span>by ${hit.authors ? hit.authors.join(', ') : 'Unknown Author'}</span><br>
        <span>Published: ${hit.publishedDate?.$date ? new Date(hit.publishedDate.$date).toLocaleDateString() : 'Unknown'}</span><br>
        <span>Categories: ${hit.categories ? hit.categories.join(', ') : 'Uncategorized'}</span>
    </div>
</div>`,
        },
    })
);
itbookSearch.start();