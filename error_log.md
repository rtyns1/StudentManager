Error logging is a skill, it forces me to slow down, capture context and avoid repeating mistakes. It will also help me learn deeper.
I need a template, and extremely well documented error logging.

---> Now, no need for notes, for my first error::

## Error #001 – ERROR: response status is 404 --error no swagger

**Date:** 2026-05-28  
**Environment:** API running on https://localhost:7073  -- swagger is showing, no fauts in the UI
**Symptom:**  
Swagger shows the endpoint, but executing POST returns 404 Not Found. The `ping` test (GET /api/ScheduledEmail/ping) works correctly.
It is the same for all endpoints, so the problem is with the endpoints. Executing POST retursn the same 404 not found error.
So, i will focus on the ScheduledEmail andthen hopefully the fi there will work for all the endpoints.

## ERROR #002 -- error:: response status is 500 / niternal server error
**Date:** 2026-05-29
**Environment:** API isnt even running this time-- in swagger UI it brnigs up the 5-- response error
**Notes::**
-At first, i thought it was because i had deleted the Database, but ensurecreated() works perfectly and another DB is generated , so that cant be the issue.
I didnt change anything it just transformed itself from error 404 to error 500, so now i have to figure out the root cause for the errors.
Now, error 500 internal server error means the server code crashed while trying to process the request. It is nt a routing error that would be 404.
- Since the DB is recreated, the API is starting and EnsureCreated() works. The crash happens when a specific controlle action is executed.
- So, i need to find the endpoint that gives 500 and what the exception is.
**SOLVE**--I had a duplicate get method in Email Controller;:
```Csharp
  [HttpGet("test")]
        public IActionResult test() => Ok("It works");
        public IActionResult Test() => Ok("Test works");
```
It was bringing the error, avoid duplicates.
But, the original 404 error si not yet solved at all, so we still haev to deal with that one.

## ERROR #003 -- Error 404-- endpoint still isnt being reached,, routing mismatch






**Lessons learned:**  
(To be filled)