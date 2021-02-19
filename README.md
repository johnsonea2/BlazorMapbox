# Blazor Mapbox

## Introduction
---
This is a Blazor library that wraps the [Mapbox GL JS](https://www.mapbox.com/mapbox-gljs) library. Mapbox GL JS is a JavaScript library for vector maps on the Web. Its performance, real-time styling, and interactivity features set the bar for anyone building fast, immersive maps on the web.

## Getting Started
---
### Prerequisites & Installation 
---
- A Mapbox access token is required to view the map. You can acquire one at https://account.mapbox.com/access-tokens/. 
- Your application needs to target .NET 5. 

### Usage 
---
- Add a using statement of `@using Blazor.Mapbox` to the top of your page. 
- Add the component anywhere in the markup similar to: 
```
<MapboxMap AccessToken="{YOUR ACCESS TOKEN HERE}"></MapboxMap>
```

And that's it! The map should render on your page. 

