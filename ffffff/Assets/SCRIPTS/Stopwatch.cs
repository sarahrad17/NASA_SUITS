using System.Diagnostics;
using System.IO;
 
    private float timeElapsed;
 //Create a timer
    void Start()
    {
    public Stopwatch timer = new StopWatch();
    }
 
 //Use Timer
    void update()
    {
    //condition to start timer - voice command
    if()
        timer.Start();
        timeElapsed = timer.Elapsed;
        }
    }

    void stop()
    {
        //condition to stop timer - voice command
        if(){
            timer.Stop();
            String recordTime = "" + timeElapsed;
            //prompt user for fileName
            String fileName = ""
            writeFile(recordTime, fileName);

        }
        
    }

 //Cleanup   
    void close(){
        //voice command to close timer
        if()
            timer.End();
    }

//Helper method to write time elapsed to text file 
//Need to update where the file is getting saved though!
    private void writeFile(String recordTime, String name){
        String fileName = "C:\Users\Public\TestFolder\\" + name + ".txt";
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true))
        {
            file.WriteLine(recordTime);
        }
        
    }

    