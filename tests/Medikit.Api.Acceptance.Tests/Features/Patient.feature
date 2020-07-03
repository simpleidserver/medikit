Feature: Patient

Scenario: Add patient
	When execute HTTP POST JSON request 'http://localhost/patients'
	| Key            | Value                                                                      |
	| firstname      | firstname                                                                  |
	| lastname       | lastname                                                                   |
	| niss           | niss                                                                       |
	| gender         | 1                                                                          |
	| eid_cardnumber | eidcardnumber                                                              |
	| address        | { street: 'street', street_number: 2, postal_code: '1000', country: 'BE' } |
	| contact_infos  | [ { type: 0, value : 'toto@mail.com' } ]                                   |

	And extract JSON from body
	And extract 'id' from JSON body into 'id'
	And execute HTTP GET request 'http://localhost/patients/$id$'
	And extract JSON from body
	Then HTTP status code equals to '200'
	Then JSON 'firstname'='firstname'
	Then JSON 'lastname'='lastname'
	Then JSON 'eid_cardnumber'='eidcardnumber'
	Then JSON 'gender'='1'
	Then JSON 'niss'='niss'
	Then JSON 'contact_infos[0].type'='0'
	Then JSON 'contact_infos[0].value'='toto@mail.com'
	Then JSON 'address.postal_code'='1000'
	Then JSON 'address.country'='BE'
	Then JSON 'address.street'='street'
	Then JSON 'address.street_number'='2'


Scenario: Get patient by NISS
	When execute HTTP GET request 'http://localhost/patients/niss/071089'
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