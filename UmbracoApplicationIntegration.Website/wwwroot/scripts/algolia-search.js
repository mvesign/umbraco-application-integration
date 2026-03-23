// Initialize Algolia client
const { applicationId, searchApiKey } = window.algoliaSettings;
const searchClient = algoliasearch(applicationId, searchApiKey);

// Initialize Algolia InstantSearch
const bookSearch = instantsearch({
    indexName: 'BooksIndex',
    searchClient,
    searchFunction(helper) {
        const query = helper.state.query;

        if (query && query.length >= 3) {
            helper.search(); // Perform the search if query has 3 or more characters
        } else {
            // Clear the hits container if the query is less than 3 characters
            document.querySelector('#book-hits').innerHTML = '';
        }
    },
});

// Add a search box widget
bookSearch.addWidget(
    instantsearch.widgets.searchBox({
        container: '#search-box-books',  // HTML ID for the search box container
        placeholder: 'Search for books...',
    })
);

// Add a hits (results) widget
bookSearch.addWidget(
    instantsearch.widgets.hits({
        container: '#book-hits',  // HTML ID for the search results container
        templates: {
            item: (hit) => `
        <div>
  <h2><a href="${hit.url}">${hit.data.title}</a></h2>
  <p>by ${hit.data.author}</p>
</div>      `,
        },
    })
);

// Start the search interface
bookSearch.start();