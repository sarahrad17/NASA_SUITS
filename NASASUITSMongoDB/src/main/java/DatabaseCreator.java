import com.mongodb.*;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import com.google.gson.JsonElement;
import com.google.gson.JsonObject;
import com.google.gson.JsonParser;
import com.google.gson.*;

import java.util.ArrayList;
import java.util.HashMap;

import com.mongodb.util.JSON;
import org.bson.Document;


public class DatabaseCreator {
    public static void main(String[] args) {
        DatabaseCreator d = new DatabaseCreator();
    }

    public DatabaseCreator(){
        createDatabase();
    }

    public void createDatabase(){
        //add to MongoDB
        System.out.println("adding to MongoDB");
        MongoClient mongoClient = new MongoClient("localhost", 27017);
        MongoDatabase db = mongoClient.getDatabase("NASA-SUITS");
        MongoCollection collection = db.getCollection("Instructions");

        ArrayList<String> i_one = new ArrayList<String>();
        i_one.add("Get 6 lemons from the refridgerator");
        i_one.add("Wash the lemons");
        i_one.add("Cut the lemons in half");
        i_one.add("Squeeze all the lemons into a pitcher");
        i_one.add("Add water");
        i_one.add("Add sugar until you're not a sour boi anymore");
        i_one.add("TADA LEMONADE!");

        Instruction lemonade = new Instruction("Lemonade", 69420, i_one);

        ArrayList<String> two = new ArrayList<String>();
        two.add("Get a big rocket");
        two.add("Make sure big rocket does not explode");
        two.add("Strap people ontop of the big rocket");
        two.add("Light the big rocket (on fire) and hope it goes up");
        two.add("TADA YOU MIGHT BE IN SPACE NOW!!! or dead lol");
        Instruction tospace = new Instruction ("SpaceFlight", 31415, two);

        Gson gson = new Gson();
        String LemonadeJson = gson.toJson(lemonade);
        JsonObject lemonadeJson = new JsonParser().parse(LemonadeJson).getAsJsonObject();
        String id = lemonadeJson.get("id").getAsString();
        lemonadeJson.addProperty("_id", id);
        Document doc = Document.parse(lemonadeJson.toString());
        try {
            collection.insertOne(doc);
        } catch (Exception e) {
            e.printStackTrace();
            System.out.println("Avoided Duplicate");
        }






    }
}
