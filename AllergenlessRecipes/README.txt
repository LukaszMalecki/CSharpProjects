Project start: 11.01.2024

New project idea for an app which allows users to have a database of recipes with all their possible allergens listed and makes it possible to filter any them.
The idea stems from the fact that food allergies become more and more common and they don't include only dairy products, nuts, etc. They also include foods such as paprica, onion, garlic, apple, pork and the list goes on. People also go on specific diets for health/worldview reasons so the app could also include diets such as vegan, vegetarian, keto.
Therefore an app such as this needs to be as flexible in filtering and adding new filters as possible.

As the first order of businness I have to collect functional and non-functional requirements for my app.
Moreover, the data structure for such an app needs to be thoroughly considered as typical inheritance can't account for a product being in many categories at once (such as mayo being made of both oil and egg yolk at once, products having labels such as vegan, low fodmap).

App's main language will be Polish and possibility of adding English language is considered. 

For now the idea is to have hierarchy looking like this for example:
Warzywa <- Rośliny psionkowate <- Papryka <- Papryka czerwona
And of example Papryka could be non-abstract, cuz sometimes it doesn't matter if it's red or yellow bell pepper.
Moreover, it's possible that all of these will also be stored in one table so as all of them have unique IDs. The categories of Produkty mleczne (Dairy) or Mięso (Meat) would be useful, because filtering out for example Dairy would automatically filter out milk, butter and the like.
As there are more complex products such as mayonnaise or ketchup, they could either be made to fit in many categories or be considered a collection of their primary ingredients.

The problem is complex, especially when types of products can vary, but some level of simplification of a problem is inevitable when we try to make it into data.

As the project lacks funding and professional food experts, it most likely won't have a full database. But it will have enough to be somewhat useful and will provide means to broaden the selection of ingredients.

Before any coding happens, a lot of thinking is due.
