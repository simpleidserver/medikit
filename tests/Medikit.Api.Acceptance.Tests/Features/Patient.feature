Feature: Patient

Scenario: Get patient by NISS
	When execute HTTP GET request 'http://localhost/patients/071089'
	And extract JSON from body
	Then HTTP status code equals to '200'
	Then JSON 'firstname'='thierry'

Scenario: Search patients
	When execute HTTP GET request 'http://localhost/patients/.search?firstname=thierry&niss=071089&lastname=habart'
	And extract JSON from body
	Then HTTP status code equals to '200'
	Then JSON 'start_index'='0'
	Then JSON 'count'='100'
	Then JSON 'total_length'='1'
	Then JSON 'content[0].firstname'='thierry'
	Then JSON 'content[0].lastname'='habart'
	Then JSON 'content[0].niss'='071089'