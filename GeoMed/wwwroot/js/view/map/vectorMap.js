



am4core.ready(function () {

    swal(
        {
            title: "USA Map", text: "Please wait while the data is processed", allowOutsideClick: false,
            onOpen: function () { swal.showLoading() }
        });

    var populations = [
        {id:'US-AL', value: 4887871,  },
        {id:'US-AK', value: 737438,   },
        {id:'US-AZ', value: 7171646,  },
        {id:'US-AR', value: 3013825,  },
        {id:'US-CA', value: 39557045, },
        {id:'US-CO', value: 5695564,  },
        {id:'US-CT', value: 3572665,  },
        {id:'US-DE', value: 967171,   },
        {id:'US-DC', value: 702455,   },
        {id:'US-FL', value: 21299325, },
        {id:'US-GA', value: 10519475, },
        {id:'US-HI', value: 1420491,  },
        {id:'US-ID', value: 1754208,  },
        {id:'US-IL', value: 12741080, },
        {id:'US-IN', value: 6691878,  },
        {id:'US-IA', value: 3156145,  },
        {id:'US-KS', value: 2911505,  },
        {id:'US-KY', value: 4468402,  },
        {id:'US-LA', value: 4659978,  },
        {id:'US-ME', value: 1338404,  },
        {id:'US-MD', value: 6042718,  },
        {id:'US-MA', value: 6902149,  },
        {id:'US-MI', value: 9995915,  },
        {id:'US-MN', value: 5611179,  },
        {id:'US-MS', value: 2986530,  },
        {id:'US-MO', value: 6126452,  },
        {id:'US-MT', value: 1062305,  },
        {id:'US-NE', value: 1929268,  },
        {id:'US-NV', value: 3034392,  },
        {id:'US-NH', value: 1356458,  },
        {id:'US-NJ', value: 8908520,  },
        {id:'US-NM', value: 2095428,  },
        {id:'US-NY', value: 19542209, },
        {id:'US-NC', value: 10383620, },
        {id:'US-ND', value: 760077,   },
        {id:'US-OH', value: 11689442, },
        {id:'US-OK', value: 3943079,  },
        {id:'US-OR', value: 4190713,  },
        {id:'US-PA', value: 12807060, },
        {id:'US-RI', value: 1057315,  },
        {id:'US-SC', value: 5084127,  },
        {id:'US-SD', value: 882235,   },
        {id:'US-TN', value: 6770010,  },
        {id:'US-TX', value: 28701845, },
        {id:'US-UT', value: 3161105,  },
        {id:'US-VT', value: 626299,   },
        {id:'US-VA', value: 8517685,  },
        {id:'US-WA', value: 7535591,  },
        {id:'US-WV', value: 1805832,  },
        {id:'US-WI', value: 5813568,  },
        {id:'US-WY', value: 577737,   },
        {id:'US-PR', value: 3195153 },
    ];


    am4core.useTheme(am4themes_animated);
    var chart = am4core.create("usaMap", am4maps.MapChart);
    chart.maxZoomLevel = 64;
    chart.logo.height = -20;
    chart.rtl = true;
    chart.geodata = am4geodata_usaHigh;
    chart.projection = new am4maps.projections.AlbersUsa();
    var zoomOut = chart.tooltipContainer.createChild(am4core.ZoomOutButton);
    zoomOut.align = "right";
    zoomOut.valign = "top";
    zoomOut.margin(20, 20, 20, 20);
    zoomOut.events.on("hit", function () {
        if (currentSeries) {
            currentSeries.hide();
        }
        chart.goHome();
        zoomOut.hide();
        currentSeries = regionalSeries.US.series;
        currentSeries.show();
    });
    zoomOut.hide();



    // Create map polygon series
    var polygonSeries = chart.series.push(new am4maps.MapPolygonSeries());
    polygonSeries.useGeodata = true;
    polygonSeries.calculateVisualCenter = true;



    var polygonTemplate = polygonSeries.mapPolygons.template;
    polygonTemplate.tooltipText = "[bold]{name} \n population: {value} [/]";
    polygonTemplate.fill = am4core.color("#E76275");
    //polygonTemplate.fill = chart.colors.getIndex(0);
    //polygonTemplate.nonScalingStroke = true;
    //polygonTemplate.strokeWidth = 0.5;

    var hs = polygonTemplate.states.create("hover");
    hs.properties.fill = "#DB1430";

    polygonSeries.heatRules.push({
        "property": "fill",
        "target": polygonSeries.mapPolygons.template,
        "min": am4core.color("#F9DBE0"),
        "max": am4core.color("#DB1430")
    });

    polygonSeries.data = populations;

    // Load data when map polygons are ready
    chart.events.on("ready", loadCases);

    // Loads store data
    function loadCases() {
        var loader = new am4core.DataSource();
        loader.url = "https://localhost:44363/Map/USA_Covid";
        loader.events.on("parseended", function (ev) {

            if (ev.target.data[0] === '[') {
                var _data = JSON.parse(ev.target.data);
                setupStores(_data.filter(x => x.cases > 0));
            } else {
                setupStores(ev.target.data.filter(x => x.cases>0));
            }
          //  console.log("data", ev.target.data)
          //  setupStores(ev.target.data);
        });
        loader.load();
    }

    // Creates a series
    function createSeries(heatfield) {
        var series = chart.series.push(new am4maps.MapImageSeries());
        series.dataFields.value = heatfield;

        var template = series.mapImages.template;
        template.verticalCenter = "middle";
        template.horizontalCenter = "middle";
        template.propertyFields.latitude = "lat";
        template.propertyFields.longitude = "long";
        template.tooltipText = "{name}:\n[bold]{count} cases[/] \n {long} , {lat} \n {count}";

        var circle = template.createChild(am4core.Circle);
        circle.radius = 10;
        circle.fillOpacity = 0.7;
        circle.verticalCenter = "middle";
        circle.horizontalCenter = "middle";
        circle.nonScaling = true;

        var label = template.createChild(am4core.Label);
        label.text = "{cases}";
        label.fill = am4core.color("#fff");
        label.verticalCenter = "middle";
        label.horizontalCenter = "middle";
        label.nonScaling = true;

        var heat = series.heatRules.push({
            target: circle,
            property: "radius",
            min: 10,
            max: 40
        });

        // Set up drill-down
        series.mapImages.template.events.on("hit", function (ev) {

            // Determine what we've clicked on
            var data = ev.target.dataItem.dataContext;
            // No id? Individual store - nothing to drill down to further
            if (!data.target) {
                return;
            }
            // Create actual series if it hasn't been yet created
            if (!regionalSeries[data.target].series) {
                regionalSeries[data.target].series = createSeries("count");
                regionalSeries[data.target].series.data = data.markerData;
            }
            // Hide current series
            if (currentSeries) {
                currentSeries.hide();
            }
            // Control zoom
            if (data.type == "state") {

                var statePolygon = polygonSeries.getPolygonById("US-" + data.state);
                chart.zoomToMapObject(statePolygon);
            }
            else if (data.type == "country") {

                chart.zoomToGeoPoint({
                    latitude: data.lat,
                    longitude: data.long
                }, 64, true);
            }
            zoomOut.show();

            // Show new targert series
            currentSeries = regionalSeries[data.target].series;

            currentSeries.show();
        });

        return series;
    }

    var regionalSeries = {};
    var currentSeries;

    function setupStores(data) {

        // Init country-level series
        regionalSeries.US = {
            markerData: [],
            series: createSeries("count")
        };

        // Set current series
        currentSeries = regionalSeries.US.series;

        // Process data
        am4core.array.each(data, function (model) {

            var model = {
                state: model.state,
                StateCode: model.stateCode,
                long: am4core.type.toNumber(model.long),
                lat: am4core.type.toNumber(model.lat),
                country: model.country,
                count: am4core.type.toNumber(model.cases)
            };

            // Process state-level data
            if (regionalSeries[model.StateCode] == undefined) {
                var statePolygon = polygonSeries.getPolygonById("US-" + model.StateCode);

                if (statePolygon) {

                    // Add state data
                    regionalSeries[model.StateCode] = {
                        target: model.StateCode,
                        type: "state",
                        name: statePolygon.dataItem.dataContext.name,
                        count: model.count,
                        cases: 1,
                        lat: statePolygon.visualLatitude,
                        long: statePolygon.visualLongitude,
                        state: model.StateCode,
                        markerData: []
                    };
                    regionalSeries.US.markerData.push(regionalSeries[model.StateCode]);

                }
                else {
                    // State not found
                    return;
                }
            }
            else {
                regionalSeries[model.StateCode].cases++;
                regionalSeries[model.StateCode].count += model.count;
            }

            // Process city-level data
            if (regionalSeries[model.country] == undefined) {
                regionalSeries[model.country] = {
                    target: model.country,
                    type: "country",
                    name: model.country,
                    count: model.count,
                    cases: 1,
                    lat: model.lat,
                    long: model.long,
                    state: model.StateCode,
                    markerData: []
                };
                regionalSeries[model.StateCode].markerData.push(regionalSeries[model.country]);
            }
            else {
                regionalSeries[model.country].cases++;
                regionalSeries[model.country].count += model.count;
            }

            // Process individual store
            regionalSeries[model.country].markerData.push({
                name: model.country,
                count: model.count,
                cases: 1,
                lat: model.lat,
                long: model.long,
                state: model.StateCode
            });

        });

        regionalSeries.US.series.data = regionalSeries.US.markerData;

        swal.close()
    }

}); // end am4core.ready()
