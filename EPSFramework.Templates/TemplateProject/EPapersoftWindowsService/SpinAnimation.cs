//----------------------------------------------------------------------------------------------
// <copyright file="$fileinputname$" author="http://www.c-sharpcorner.com/members/colin92">
// Copyright(C) $year$ www.c-sharpcorner.com
// </copyright>
//<license>
//  This program is free software: you can redistribute it and/or modify it under the terms of 
//  the GNU General Public License as published by the Free Software Foundation, either version 
//  3 of the License, or (at your option) any later version.
//  This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; 
//  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
//  See the LICENSE file in the project root for more information or
//  visit, see<http://www.gnu.org/licenses/>.
//</license>
//----------------------------------------------------------------------------------------------
using System;

public static class SpinAnimation
{
    //spinner background thread

    private static System.ComponentModel.BackgroundWorker spinner = initialiseBackgroundWorker();

    //starting position of spinner changes to current position on start

    private static int spinnerPosition = 25;

    //pause time in milliseconds between each character in the spin animation

    private static int spinWait = 25;

    //field and property to inform client if spinner is currently running

    private static bool isRunning;

    public static bool IsRunning { get { return isRunning; } }

    /// <summary>

    /// Worker thread factory

    /// </summary>

    /// <returns>background worker thread</returns>

    private static System.ComponentModel.BackgroundWorker initialiseBackgroundWorker()

    {
        System.ComponentModel.BackgroundWorker obj = new System.ComponentModel.BackgroundWorker();

        //allow cancellation to be able to stop the spinner

        obj.WorkerSupportsCancellation = true;

        //anonymous method for background thread's DoWork event

        obj.DoWork += delegate

        {
            //set the spinner position to the current console position

            spinnerPosition = Console.CursorLeft;

            //run animation unless a cancellation is pending

            while (!obj.CancellationPending)

            {
                //characters to iterate through during animation

                char[] spinChars = new char[] { '|', '/', '-', '\\' };

                //iterate through animation character array

                foreach (char spinChar in spinChars)

                {
                    //reset the cursor position to the spinner position

                    Console.CursorLeft = spinnerPosition;

                    //write the current character to the console

                    Console.Write(spinChar);

                    //pause for smooth animation - set by the start method

                    System.Threading.Thread.Sleep(spinWait);
                }
            }
        };

        return obj;
    }

    /// <summary>

    /// Start the animation

    /// </summary>

    /// <param name="spinWait">wait time between spin steps in milliseconds</param>

    public static void Start(int spinWait)

    {
        //Set the running flag

        isRunning = true;

        //process spinwait value

        SpinAnimation.spinWait = spinWait;

        //start the animation unless already started

        if (!spinner.IsBusy)

            spinner.RunWorkerAsync();
        else throw new InvalidOperationException("Cannot start spinner whilst spinner is already running");
    }

    /// <summary>

    /// Overloaded Start method with default wait value

    /// </summary>

    public static void Start()
    {
        Start(25);
    }

    /// <summary>

    /// Stop the spin animation

    /// </summary>

    public static void Stop()

    {
        //Stop the animation

        spinner.CancelAsync();

        //wait for cancellation to complete

        while (spinner.IsBusy) System.Threading.Thread.Sleep(100);

        //reset the cursor position

        Console.CursorLeft = spinnerPosition;

        //set the running flag

        isRunning = false;
    }
}