Task 6.

The task was to include todo lists than the current user had an item that they were responsible for. This meant it was possible for the
user to not see lists that they created, because all items were assigned to someone else.
I've implemented this so that it includes the union of lists that they own, and lists that they have items in.

Task 7.

Sorting is done via a javascript sort of the <li> elements in the list, passing a comparitor function and an ascending flag. The comparitor looks at the data-importance or data-rank attributes of each <li> element, which were added when each todo item was added to the list.

Sorting is actioned via clicking either of the two links at the top, 'importance' or 'rank'. It defaults to importance/ascending. Clicking the same link a second time reverses the sort order, though no indication of the current sort order has been added.

Navigating away from the view, to create or edit, returns to the default sort order of importance/ascending when the page is reloaded.

If the list is empty, removing the sort links should be considered, or, disabling them. 

Task 8.

The view is loaded with an empty <p> element where the profile name will go (if it exists). This is then populated by calling loadProfileNames(), which iterates through the list looking for these paragraph elements and calling the controller asynchronously to request it from gravatar. If it does not exist, or the call to gravatar fails for any http-related reason (for instance if the service is down), an empty string is returned to the browser.

Task 9.

I've removed the Add New Item link, but left the Create Item view and supporting code in-place as the New List functionality redirects there after creating a list.
I've added a button 'create item' that is not triggered by pressing return, as I did not want any accidental creations - there is currently no way of removing a list. The user only needs to add the title, ownership defaults to the list owener, with medium importance and zero rank.

Task 10.

I've added up/down buttons to each item, that are only enabled when sorted by rank (cannot reorder rank properly when sorted by importance).
Each item is now give a unique rank starting from 0.
Any pre-existing data will need to be removed when running this version, as the code has been updated to give each item an incrementing rank starting from 0. This way, pressing the UP or DOWN button on each item simply swaps the rank with the sibling item, and resorts.
As well as re-ordering on the page, an asychronous call is made to the controller to swap the ranks in the database.
Any pre-existing data will need to be removed before running this version, as the ability to edit the rank in the edit page has also been removed - the importance of maintaining a unique rank is paramount for this version of reordering to work.

I could have made the LI elements draggable, but the requirements didn't state what was visually required. Also, this only requires swapping the ranks of two items, draggable elements would mean updating the ranks of potentially many records (though it is possible by using a double for the rank, or having the initial ranks spaced out).

Issues: when creating an item, the spacing after the buttons is not correct (Chrome Version 109.0.5414.120, Edge Version 125.0.2535.79)