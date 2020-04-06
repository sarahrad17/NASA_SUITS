import requests 
  
# api-endpoint 
URL = "http://localhost:3000/api/suit/"
  
# sending get request and saving the response as response object 
r = requests.get(URL) 
  
# extracting data in json format 
data = r.json() 
 
print(data)
