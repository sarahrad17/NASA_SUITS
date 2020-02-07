import java.util.ArrayList;
import java.util.HashMap;
import java.util.LinkedHashMap;

import java.io.Serializable;

public class Instruction implements Serializable {

    private static final long serialVersionUID = 1L;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public ArrayList<String> getInstructions() {
        return instructions;
    }

    public void setInstructions(ArrayList<String> instructions) {
        this.instructions = instructions;
    }

    private String name;
    private int id;
    private ArrayList<String> instructions = new ArrayList<String>();

    public Instruction(String name, int id, ArrayList<String> instructions){
        this.name = name;
        this.id = id;
        this.instructions = instructions;
    }



}
