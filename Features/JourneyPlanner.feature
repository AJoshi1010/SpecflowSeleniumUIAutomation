@TLF
Feature: Plan a Journey
	TFL Journey Planner widget assists users to explore all the available routs and services

	Scenario: Verify that widget is unable to plan a journey if no locations are entered
		Given the locations are not provided
		When the journey is planned
		Then a message 'The From field is required.' should be displayed
		And a message 'The To field is required.' should be displayed

	Scenario Outline: Verify that error is displayed when an invalid journey is planned
		Given the from location is <From>
		And the to location is <To>
		When the journey is planned
		Then a message '<Text>' should be displayed
		Examples:
			| From          | To  | Text                                                                        |
			| London Bridge | 775 | Journey planner could not find any results to your search. Please try again |
			| 1010          | 007 | Journey planner could not find any results to your search. Please try again |
			| West Croydon  | GVS | Sorry, we can't find a journey matching your criteria                       |

	Scenario: Verify that user is able to plan a journey for valid locations
		Given the from location is Clapham Junction
		And the to location is London Bridge
		When the journey is planned
		Then a message 'Fastest by public transport' should be displayed

	Scenario: Verify that user is able to edit a journey for valid locations
		Given the from location is East Croydon
		And the to location is London Bridge
		When the journey is planned
		Then a message 'Fastest by public transport' should be displayed
		When an edit is made on current planned journey
		Given clear and update new from location as Westminster
		And clear and update new to location as London Gatwick Airport
		When the journey is planned
		Then a message 'Walking and cycling' should be displayed

	Scenario: Verify that recently planned journey for valid locations are displayed
		Given the from location is Westminster
		And the to location is London Bridge
		When the journey is planned
		Then a message 'Fastest by public transport' should be displayed
		When an edit is made on current planned journey
		Given clear and update new from location as East Croydon
		And clear and update new to location as West Croydon
		When the journey is planned
		Then a message 'Fastest by public transport' should be displayed
		When all recently planned journeys are viewed
		Then a message 'East Croydon to West Croydon' should be displayed
		And a message 'Westminster to London Bridge' should be displayed
