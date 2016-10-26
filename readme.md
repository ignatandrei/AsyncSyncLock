This application shows how to start async function that are

1. async ( the function in code is async Task<int> AsyncTask(int i)  )
2. sync - that requires that the sync finishes BEFORE starting another sync ( the function in code is async Task<int> syncTask(int i) )

in a similar way.

My results are in the results.txt - and show how the sync wait to finish, and async is interspersed

More details in code or in the wiki : https://github.com/ignatandrei/AsyncSyncLock/wiki
