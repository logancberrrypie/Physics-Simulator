using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateController {


    public static Particle CalculateControl()
    {
        Particle values = new Particle();
        //1 = 1D
        //2 = 2D
        //3 = 3D
        int dimentions = UiController.instances.DropBoxDimention.value;
        Debug.Log("Number of dimentions :" + dimentions);
        if (dimentions >= 1)
        {
            GetInput_Suvat_x(ref values);
        }
        if (dimentions >= 2)
        {
            GetInput_Suvat_y(ref values);
        }
        else
        {
            values.inValidInput[1] = true;
        }
        if (dimentions >= 3)
        {
            Debug.Log("Dimentions 3 ran");
            GetInput_Suvat_z(ref values);
            Debug.Log(values.Displacement[2]);
            Debug.Log(values.InitialVelociy[2]);
            Debug.Log(values.FinalVelocity[2]);
            Debug.Log(values.Acceleration[2]);
            Debug.Log(values.Time);
        }
        else
        {
            values.inValidInput[2] = true;
        }
        GetInput_Misc(ref values);
        Particle finsihed = ValidationCheck(ref values);
        try
        {
            Particle.Instances[UiController.instances.DropBoxParticle.value] = finsihed;
        }
        catch(ArgumentOutOfRangeException e)
        {
            Particle.Instances.Add(finsihed);
        }

        return finsihed;
    }

    private static Particle ValidationCheck(ref Particle values)
    {
        Debug.Log("Key x : " + values.Key[0]);
        Debug.Log("Key y : " + values.Key[1]);
        Debug.Log("Key z : " + values.Key[2]);


        //If one of the axis has more than or equal to 3 inputs (then the program can calculate)
        if (values.GetNumberOfInputs()[0] >= 3 || values.GetNumberOfInputs()[1] >= 3 || values.GetNumberOfInputs()[2] >= 3)
        {
            //Returns values with all values calculated
            values = SuvatSolvers.FindEquation(values);
            UiController.instances.UpdateUI(values);
            //return;
        }
        else
        {
            string msg = "You must input atleast 3 quantities in one of the dimentions";
            MessageBox(msg);
        }
        //All the dimentions are valid
        if (values.inValidInput[0] == false || values.inValidInput[1] == false || values.inValidInput[2] == false)
        {
            UiController.instances.UpdateUI(values);
            //Program has now ended
        }
        else
        {
            string msg = "Your inputs are impossible inside of the real domain.";
            MessageBox(msg);
        }
        return values;
    }

    #region Get Inputs
    private static void GetInput_Suvat_x(ref Particle values)
    {
        //Creating local instance of the object for simpler name + read only
        UiController Ui = UiController.instances;

        values.Key[0] = "";

        #region endless if statements
        if (Ui.InputField_s_x.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.Displacement[0] = float.Parse(Ui.InputField_s_x.text);
            values.Key[0] += "1";
        }
        if (Ui.InputField_u_x.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.InitialVelociy[0] = float.Parse(Ui.InputField_u_x.text);
            values.Key[0] += "1";
        }

        if (Ui.InputField_v_x.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.FinalVelocity[0] = float.Parse(Ui.InputField_v_x.text);
            values.Key[0] += "1";
        }

        if (Ui.InputField_a_x.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.Acceleration[0] = float.Parse(Ui.InputField_a_x.text);
            values.Key[0] += "1";
        }
        if (Ui.InputField_Time.text == "")
        {
            values.Key[0] += "0";
        }
        else
        {
            values.Time = float.Parse(Ui.InputField_Time.text);
            values.Key[0] += "1";
        }
        if (Ui.InputField_r_x.text != "")
        {
            values.Position[0] = float.Parse(Ui.InputField_r_x.text);
        }
        else
        {
            values.Position[0] = 0;
        }
        #endregion
    }

    private static void GetInput_Suvat_y(ref Particle values)
    {
        //Creating local instance of the object for simpler name + read only
        UiController Ui = UiController.instances;

        values.Key[1] = "";

        #region endless if statements
        if (Ui.InputField_s_y.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.Displacement[1] = float.Parse(Ui.InputField_s_y.text);
            values.Key[1] += "1";
        }
        if (Ui.InputField_u_y.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.InitialVelociy[1] = float.Parse(Ui.InputField_u_y.text);
            values.Key[1] += "1";
        }

        if (Ui.InputField_v_y.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.FinalVelocity[1] = float.Parse(Ui.InputField_v_y.text);
            values.Key[1] += "1";
        }

        if (Ui.InputField_a_y.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.Acceleration[1] = float.Parse(Ui.InputField_a_y.text);
            values.Key[1] += "1";
        }
        if (Ui.InputField_Time.text == "")
        {
            values.Key[1] += "0";
        }
        else
        {
            values.Time = float.Parse(Ui.InputField_Time.text);
            values.Key[1] += "1";
        }
        if (Ui.InputField_r_y.text != "")
        {
            values.Position[1] = float.Parse(Ui.InputField_r_y.text);
        }
        else
        {
            values.Position[1] = 0;
        }
        #endregion
    }

    private static void GetInput_Suvat_z(ref Particle values)
    {
        //Creating local instance of the object for simpler name + read only
        UiController Ui = UiController.instances;

        values.Key[2] = "";

        #region endless if statements
        if (Ui.InputField_s_z.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.Displacement[2] = float.Parse(Ui.InputField_s_z.text);
            values.Key[2] += "1";
        }
        if (Ui.InputField_u_z.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.InitialVelociy[2] = float.Parse(Ui.InputField_u_z.text);
            values.Key[2] += "1";
        }

        if (Ui.InputField_v_z.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.FinalVelocity[2] = float.Parse(Ui.InputField_v_z.text);
            values.Key[2] += "1";
        }
        if (Ui.InputField_a_z.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.Acceleration[2] = float.Parse(Ui.InputField_a_z.text);
            values.Key[2] += "1";
        }
        if (Ui.InputField_Time.text == "")
        {
            values.Key[2] += "0";
        }
        else
        {
            values.Time = float.Parse(Ui.InputField_Time.text);
            values.Key[2] += "1";
        }
        if (Ui.InputField_r_z.text != "")
        {
            values.Position[2] = float.Parse(Ui.InputField_r_z.text);
        }
        else
        {
            values.Position[2] = 0;
        }
        #endregion
    }


    private static void GetInput_Misc(ref Particle values)
    {
        //Creating local instance of the object for simpler name + read only
        UiController Ui = UiController.instances;

        if (Ui.InputField_Mass.text != "")
        {
            values.Mass = float.Parse(Ui.InputField_Mass.text);
        }
        else
        {
            values.Mass = 1;
        }
        values.Restitution = Ui.SliderRestitution.value;
        if (Ui.InputField_Radius.text != "")
        {
            values.Radius = float.Parse(Ui.InputField_Radius.text);
        }
        else
        {
            values.Radius = 1;
        }
    }

    #endregion


    private static void MessageBox(string msg)
    {
        Debug.Log(msg);
        //throw new NotImplementedException();
    }

}
