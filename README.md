# BrAIn-Dead - Archived/Abandoned Project
This is the README.md of an app I created for computer science, I have decided to abandon this project because of the short-sighted, short-term and thereby uninformed solutions/decision I have made, which certainly will pave the path I walk just a little bit further; but at what point will those exact foundations break down and make this project so bad, unstable and unusable that a complete redesign would be required to even make the _idea_ decent.

#### This is just an explanation on why this whole project is so shit. The entire reason I posted this on here even though it's an abandoned project is because I want other people to learn from my mistakes, I hope I formulated all the mistakes I made well enough to make that possible.

### Reasons for abandoning it

* Tried C++ instead of C#<br>
For about a week (at the beginning of the project) I had tried to get C++ to work with UWP and WinForms because this was a language and environment that I still found difficult but was already somewhat familiar and comfortable with. C# was a language from Microsoft explicitly created to replace C++ in GUI programming, at the emergence in the .NET framework of Windows 98, ME, NT 4. 0, 2000, and XP. There was an attempt to make a C++ framework for this but it wasn't good enough and since Microsoft was still living under the slogan "Embrace, Extend, Extinguish" they didn't want to go to Java and give them influence over Windows, so they chose to develop their own language. This also means that support for C++ in .NET had already been abandoned before 2005, and it was not only stupid but also enormously arrogant to still try to make a good-working app in it. I found this out after no documentation or examples could be found about _the class object to call `::FindName()` for the XAML DOM_. This epiphany took 2+ weeks to realize.

* I made it more difficult than it needed to be<br>
I could have finished this project much faster and the end product would have been much better if I had made this in a different framework and environment than I ended up making it. I could have easily made this in say `electron.js`, but since I didn't want a desktop app to be built on the same as a webapp I decided to make it a lot harder on myself by making it in C# with a .NET framework environment (at first even in WinForms, see next point).

* UWP instead of WinForms<br>
I started this project in WinForms; I thought it looked bad because WinForms - to the best of my understanding - doesn't have (default) dynamic DPI handling, so it automatically compiles to 32 bit, which made it look blurry and just all-round bad - I hadn't created any back-end systems at all, just tried to recreate the design I had, which is why I made the choice to switch environments instead of making extreme local changes that would have also made all 32-bit platforms unusable. As a result, I decided to switch to an environment called UWP (Universal Windows Platform), which came out in 2002 but looked better. I didn't read as much about this as I originally would have liked, so I didn't find out until too late that it had huge limitations, including no broad filesystem access, which means I couldn't download my API. I also didn't want to provide a bash or C++ script along with the app itself that would set this sort of thing up because by doing so I would have come up with another short-term solution that is absolutely not elegant and would make it so I would run into problems again later (how do I make the API run automatically with the proper read/write access permissions without the app calling the process having those itself, etc) that I wouldn't be able to answer.

* `pkg` module<br>
I had to compile my API into executable format because I didn't want this app to be over the 2gb by including LTS Node.js with it. I tried to do this with the `pkg` module, unfortunately it didn't support a huge amount of modules and I had to port it back to Node.js v6 to get it to pack modules into the executables (so I don't have to supply a node-modules folder with it), for this I had to manually restore a lot of modules and get them supported by v6. Since too many modules had to be changed I decided to remove many routes from my API. Unfortunately, even version alpha 0.7 of puppeteer (headless version of the Chrome browser) didn't work and so I had to remove even the most important routes.

* A lot of hardcoded programming<br>
Because I am not used to the .NET environment, the XAML DOM and C#, many things are hardcoded programming. I like to keep my codes as compact and softcoded as possible. I had planned to give the buttons names that corresponded to the XAML documents, return the name of the button pressed to the C# script and have the corresponding DOM loaded. Unfortunately, it was not possible to call an object via the name in string format in C#, as you would do in Node.js with `global["name"]` this is not possible. Because of this, I had to create a `switch ... case` conditional loop, which made my script look a lot worse.

* MSDN documentation<br>
Whenever I ran into something, I was actually required to immediately ask other people for help or look at examples that had extremely specific or no context (`this::FindName()`), because the MSDN (Micro-Soft Developer Network) documentation was virtually impossible to work with. They show the datatype of the return value of the function and they tell what the function does but nothing else. This makes it extremely difficult to get around as a beginner (beginner in C#, .NET and all Windows programming really) and demands an extreme amount of time and motivation. I didn't have a massive amount of time for this project.

* UWP platform = dead<br>
After abandoning this project I think it's safe for me to conclude that the UWP environment is just flat-out dead. There's next to no support for it on forums and tons of people have switched to GUI-based languages that can be made cross-platform instead of GUI-based environments that basically only work on Windows. As a result of this I would really not recommend anyone to try to make an app in UWP with the expectations of it working seamlessly.

### Plans

So because of all this, it didn't work out. I did have grand plans for this project:

* Connect to my API<br>
Whether it was hosted by my app itself or not, the whole idea behind this app was to connect it to my API. This shouldn't have been too difficult but it would be one of the first official tests of my API.

* PNG image loader<br>
My API has a `/colorpalette/:color` route and outputs a color palette preview PNG, encoded in Base64. I was planning to make an PNG image loader for this, this was one of the parts I was most looking forward to making/researching.

* Markdown notes tab<br>
Under the "Notes" tab, I planned to make a large text box where if you typed text, it would show up as markdown.

* Save to which tab the program is closed<br>
This part shouldn't be too tricky, with every tab opened you give `localSettings[key]` a certain value and the value it has when the app is opened is the tab it should open (since there are no startup or close events _contradictary to MSDN's documentation_). Unfortunately, I found out the other day that `localSettings[key]` doesn't work reliably on all Windows devices<br><br>**editor's note: it's too late now to change it but here *yet again* MSDN's incompetence is on display. The documentation clearly states that you only need to provide a key and value like a normal object (`localSettings["key"] = "value"`), but after doing quite a bit of research it turns out that you need to explicitly set it first (`localSettings.Values.Add("key", "value")`). If I had found this earlier or if it had appeared in the documentation I could have been able to get rid of the bug of going back to the home page and getting the API dialog again.**

In conclusion, I made everything way too hard for myself and too many quick and underinformed decisions made it so everything would work for the time being but after that everything would just collapse. Next time I might look into programming everything in C# purely or just making it easier on myself by using Electron-js or something like that.
