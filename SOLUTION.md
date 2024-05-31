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