// SPDX-License-Identifier: Apache-2.0

/*
  Sample Chaincode based on Demonstrated Scenario

 This code is based on code written by the Hyperledger Fabric community.
  Original code can be found here: https://github.com/hyperledger/fabric-samples/blob/release/chaincode/fabcar/fabcar.go
 */

package main

/* Imports  
* 4 utility libraries for handling bytes, reading and writing JSON, 
formatting, and string manipulation  
* 2 specific Hyperledger Fabric specific libraries for Smart Contracts  
*/ 
import (
	"bytes"
	"encoding/json"
	"fmt"
	"strconv"

	"github.com/hyperledger/fabric/core/chaincode/shim"
	sc "github.com/hyperledger/fabric/protos/peer"
)

// Define the Smart Contract structure
type SmartContract struct {
}

/* Define Tuna structure, with 4 properties.  
Structure tags are used by encoding/json library
*/
type Obra struct {
	Imagen string `json:"image"`
	Timestamp string `json:"timestamp"`
	Location  string `json:"location"`
	Usuario  string `json:"usuario"`
	FechaCreacion string `json:"fechacreacion"`
	Descripcion string `json:"descripcion"`
	Tag string `json:"tag"`
}

/*
 * The Init method *
 called when the Smart Contract "tuna-chaincode" is instantiated by the network
 * Best practice is to have any Ledger initialization in separate function 
 -- see initLedger()
 */
func (s *SmartContract) Init(APIstub shim.ChaincodeStubInterface) sc.Response {
	return shim.Success(nil)
}

/*
 * The Invoke method *
 called when an application requests to run the Smart Contract "tuna-chaincode"
 The app also specifies the specific smart contract function to call with args
 */
func (s *SmartContract) Invoke(APIstub shim.ChaincodeStubInterface) sc.Response {

	// Retrieve the requested Smart Contract function and arguments
	function, args := APIstub.GetFunctionAndParameters()
	// Route to the appropriate handler function to interact with the ledger
	if function == "queryObra" {
		return s.queryObra(APIstub, args)
	} else if function == "initLedger" {
		return s.initLedger(APIstub)
	} else if function == "recordObra" {
		return s.recordObra(APIstub, args)
	} else if function == "queryAllObra" {
		return s.queryAllObra(APIstub)
	} 

	return shim.Error("Invalid Smart Contract function name.")
}

/*
 * The queryTuna method *
Used to view the records of one particular tuna
It takes one argument -- the key for the tuna in question
 */
func (s *SmartContract) queryObra(APIstub shim.ChaincodeStubInterface, args []string) sc.Response {

	if len(args) != 1 {
		return shim.Error("Incorrect number of arguments. Expecting 1")
	}

	ObraAsBytes, _ := APIstub.GetState(args[0])
	if ObraAsBytes == nil {
		return shim.Error("Could not locate tuna")
	}
	return shim.Success(ObraAsBytes)
}

/*
 * The initLedger method *
Will add test data (10 tuna catches)to our network
 */
func (s *SmartContract) initLedger(APIstub shim.ChaincodeStubInterface) sc.Response {
	obra := []Obra{
		Obra{Imagen : "img1.png", Location: "67.0006, -70.5476",Timestamp:"1504057825", FechaCreacion: "1504057825",Descripcion: "des1", Usuario: "Erick",Tag:"Img1"},
		Obra{Imagen : "img2.png", Location: "91.2395, -49.4594",Timestamp:"1504057825", FechaCreacion: "1504057825",Descripcion: "des2", Usuario: "Ricardo",Tag:"Img2"},
	}

	i := 0
	for i < len(obra) {
		fmt.Println("i is ", i)
		ObraAsBytes, _ := json.Marshal(obra[i])
		APIstub.PutState(strconv.Itoa(i+1), ObraAsBytes)
		fmt.Println("Added", obra[i])
		i = i + 1
	}

	return shim.Success(nil)
}

/*
 * The recordTuna method *
Fisherman like Sarah would use to record each of her tuna catches. 
This method takes in five arguments (attributes to be saved in the ledger). 
 */
func (s *SmartContract) recordObra(APIstub shim.ChaincodeStubInterface, args []string) sc.Response {

	var obra = Obra{ Imagen: args[1], Location: args[2], Timestamp: args[3], FechaCreacion: args[4], Descripcion: args[5], Usuario: args[6],Tag: args[7] }
	ObraAsBytes, _ := json.Marshal(obra)
	err := APIstub.PutState(args[0], ObraAsBytes)
	if err != nil {
		return shim.Error(fmt.Sprintf("Failed to record tuna catch: %s", args[0]))
	}

	return shim.Success(nil)
}

/*
 * The queryAllTuna method *
allows for assessing all the records added to the ledger(all tuna catches)
This method does not take any arguments. Returns JSON string containing results. 
 */
func (s *SmartContract) queryAllObra(APIstub shim.ChaincodeStubInterface) sc.Response {

	startKey := "0"
	endKey := "999"
    
	resultsIterator, err := APIstub.GetStateByRange(startKey, endKey)
	if err != nil {
		return shim.Error(err.Error())
	}
	defer resultsIterator.Close()

	// buffer is a JSON array containing QueryResults
	var buffer bytes.Buffer
	buffer.WriteString("[")

	bArrayMemberAlreadyWritten := false
	for resultsIterator.HasNext() {
		queryResponse, err := resultsIterator.Next()
		if err != nil {
			return shim.Error(err.Error())
		}
		// Add comma before array members,suppress it for the first array member
		if bArrayMemberAlreadyWritten == true {
			buffer.WriteString(",")
		}
		buffer.WriteString("{\"Key\":")
		buffer.WriteString("\"")
		buffer.WriteString(queryResponse.Key)
		buffer.WriteString("\"")

		buffer.WriteString(", \"Record\":")
		// Record is a JSON object, so we write as-is
		buffer.WriteString(string(queryResponse.Value))
		buffer.WriteString("}")
		bArrayMemberAlreadyWritten = true
	}
	buffer.WriteString("]")

	fmt.Printf("- queryAllObra:\n%s\n", buffer.String())

	return shim.Success(buffer.Bytes())
}
/*
 * main function *
calls the Start function 
The main function starts the chaincode in the container during instantiation.
 */
func main() {

	// Create a new Smart Contract
	err := shim.Start(new(SmartContract))
	if err != nil {
		fmt.Printf("Error creating new Smart Contract: %s", err)
	}
}