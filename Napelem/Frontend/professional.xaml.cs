﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Napelem.Models;
using System.Net.Http;

namespace Napelem
{
    /// <summary>
    /// Interaction logic for professional.xaml
    /// </summary>

    public class ProjectComponent
    {
        public Project project { get; set; }
        public Component component { get; set; }
        public int qty { get; set; }
    }
    
    public partial class professional : Window
    {
        private Employee emp;

        void exit()
        {
            MainWindow objmainWindow = new MainWindow();
            this.Close();
            objmainWindow.Show();
        }
        public professional(Employee e)
        {
            InitializeComponent();
            LoadCompontents();
            LoadProjects();
            this.emp = e;
            
        }
        public async void LoadCompontents()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7186/");

            var response = await client.GetAsync($"api/Component/SendComponent");
            if (response.IsSuccessStatusCode == true)
            {
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<JObject>(json);
                var componentsJson = obj["value"].ToString();
                var components = JsonConvert.DeserializeObject<List<Component>>(componentsJson);
                for (int i = 0; i < components.Count; i++)
                {                   
                    assetSelectComboBox.Items.Add(components[i].componentID + " " + components[i].name);
                }
                assetGrid.ItemsSource= components;
            }
        }
        public async void LoadProjects()
        {
            ProjectIdComboBox.Items.Clear();
            ReservationComboBox.Items.Clear();
            projectIDComb.Items.Clear();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7186/");

            var response = await client.GetAsync($"api/Project/ListProjects");
            if (response.IsSuccessStatusCode == true)
            {
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<JObject>(json);
                var projectsJson = obj["value"].ToString();
                var projects = JsonConvert.DeserializeObject<List<Project>>(projectsJson);


                response = await client.GetAsync($"api/Reservation/ListReservation");
                if (response.IsSuccessStatusCode == true)
                {
                    json = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<JObject>(json);
                    var reservationJson = obj["value"].ToString();
                    var reservations = JsonConvert.DeserializeObject<List<Reservation>>(reservationJson);
                    for (int i = 0; i < projects.Count; i++)
                    {
                        for (int j = 0; j<reservations.Count; j++)
                        {
                            if (reservations[j].projectID == projects[i].projectID)
                            {
                               if( ReservationComboBox.Items.Contains(projects[i].projectID + " " + projects[i].name) != true)
                                {
                                    ReservationComboBox.Items.Add(projects[i].projectID + " " + projects[i].name);
                                }
                                

                            }
                        }
                        if (projects[i].employeeID == emp.employeeID)
                        {
                            ProjectIdComboBox.Items.Add(projects[i].projectID + " " + projects[i].name);
                            projectIDComb.Items.Add(projects[i].projectID + " " + projects[i].name);
                        }

                    }
                }
                
                projectGrid.ItemsSource = projects;
            }
        }

        private void newProject_exit1(object sender, RoutedEventArgs e)
        {
            exit();
        }

        private void Project_exitBtn(object sender, RoutedEventArgs e)
        {
            exit();
        }

        private void change_status_Btn(object sender, RoutedEventArgs e)
        {
            
        }

        private async void addAssetsToProjectBtn(object sender, RoutedEventArgs e)
        {

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7186/");

            var response = await client.GetAsync($"api/Component/SendComponent");
            if (response.IsSuccessStatusCode == true)
            {
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<JObject>(json);
                var componentsJson = obj["value"].ToString();
                var components = JsonConvert.DeserializeObject<List<Component>>(componentsJson);
                Project pro = new Project();
                if (ProjectIdComboBox.Text != String.Empty && assetSelectComboBox.Text != String.Empty && qtyTextBox.Text != String.Empty)
                {
                    string[] proData = ProjectIdComboBox.Text.Split(' ');
                    pro.projectID = int.Parse(proData[0]);
                    Component comp = new Component();
                    string[] compData = assetSelectComboBox.Text.Split(' ');
                    comp.componentID = int.Parse(compData[0]);

                    ProjectComponent projectComp = new ProjectComponent();
                    projectComp.component = comp;
                    projectComp.project = pro;
                    projectComp.qty = int.Parse(qtyTextBox.Text);
                    projectComp.project.status = "Draft";

                    Log log = new Log()
                    {
                        projectID = projectComp.project.projectID,
                        status = projectComp.project.status,
                        timestamp = DateTime.Now.ToString()
                    };
                    var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(projectComp), System.Text.Encoding.UTF8, "application/json");
                    response = await client.PostAsync($"api/Reservation/AddReservation", content);
                    if (response.IsSuccessStatusCode == true)
                    {
                        MessageBox.Show("Reservation was successful.");
                    }
                    content = new StringContent(System.Text.Json.JsonSerializer.Serialize(projectComp.project), System.Text.Encoding.UTF8, "application/json");
                    response = await client.PostAsync($"api/Project/ChangeStatus", content);
                    content = new StringContent(System.Text.Json.JsonSerializer.Serialize(log), System.Text.Encoding.UTF8, "application/json");
                    response = await client.PostAsync($"api/Log/AddLog", content);
                }
                else
                {
                    MessageBox.Show("Fill all fields!", "Warning!");
                }
                
            }

            
        }

        private async void AddNewProjectBtn(object sender, RoutedEventArgs e)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7186/");          
            Project addPro = new Project();
            addPro.name = projectNameTextBox.Text;
            addPro.estimated_Time = int.Parse(workTimeTextBox.Text);
            addPro.project_description = descriptionTextBox.Text;
            addPro.project_location = projectLocationTextBox.Text;
            addPro.project_orderer = CostumerNameTextBox.Text;
            addPro.wage = 2000;
            addPro.project_price = 0;
            addPro.status = "New";
            addPro.employeeID = emp.employeeID;
            Log log = new Log()
            {
                projectID = addPro.projectID,
                status = addPro.status,
                timestamp = DateTime.Now.ToString()
            };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(addPro), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Project/AddProject", content);
            if (response.IsSuccessStatusCode == true)
            {
                MessageBox.Show("Adding project was successful.");
            }
            content = new StringContent(System.Text.Json.JsonSerializer.Serialize(log), System.Text.Encoding.UTF8, "application/json");
            response = await client.PostAsync($"api/Log/AddLog", content);
        }

        private void refreshBtn(object sender, RoutedEventArgs e)
        {
            LoadProjects();
        }

        private async void changeStatus(object sender, RoutedEventArgs e)
        {

            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7186/");
            Project pro = new Project();
            string[] proData = projectIDComb.Text.Split(' ');
            pro.projectID = int.Parse(proData[0]);
            pro.status = statusCbxBox.Text;
            Log log = new Log()
            {
                projectID = pro.projectID,
                status = pro.status,
                timestamp = DateTime.Now.ToString()
            };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(pro), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"api/Project/ChangeStatus", content);
            if (response.IsSuccessStatusCode == true)
            {
                MessageBox.Show("Status changed.");
            }
            content = new StringContent(System.Text.Json.JsonSerializer.Serialize(log), System.Text.Encoding.UTF8, "application/json");
            response = await client.PostAsync($"api/Log/AddLog", content);
        }

        private async void CalculatePrice(object sender, RoutedEventArgs e)
        {
            
            string[] projectData = ReservationComboBox.Text.Split(' ');
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7186/");
            var response = await client.GetAsync($"api/Project?projectID={projectData[0]}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var project = JsonConvert.DeserializeObject<Project>(responseBody);
           
           
            response = await client.GetAsync($"api/Reservation/ListReservation");
            if (response.IsSuccessStatusCode == true)
            {
                var json = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<JObject>(json);
                var reservationJson = obj["value"].ToString();
                var reservations = JsonConvert.DeserializeObject<List<Reservation>>(reservationJson);
                response = await client.GetAsync($"api/Component/SendComponent");
                if (response.IsSuccessStatusCode == true)
                {
                    json = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<JObject>(json);
                    var componentsJson = obj["value"].ToString();
                    var components = JsonConvert.DeserializeObject<List<Component>>(componentsJson);
                    project.project_price = 0;
                    project.project_price += project.wage * project.estimated_Time;
                    for (int i = 0; i < reservations.Count; i++)
                    {
                        for (int j = 0; j < components.Count; j++)
                        {
                            if (reservations[i].projectID == project.projectID && reservations[i].componentID == components[j].componentID)
                            {
                                project.project_price += reservations[i].reservationQuantity * components[j].price ;
                            }
                        }
                    }
                    EstimatedPrice.Content = project.project_price.ToString();
                    ProjectComponent projectComp = new ProjectComponent();
                    projectComp.project = project;
                    bool wait = false;
                    for (int i = 0; i < reservations.Count; i++)
                    {
                        for (int j = 0; j < components.Count; j++)
                        {
                            if (reservations[i].projectID == projectComp.project.projectID && reservations[i].componentID == components[j].componentID)
                            {
                                projectComp.component = components[j];
                                if (components[j].componentID == reservations[i].componentID)
                                {
                                    if (components[j].quantity < reservations[i].reservationQuantity)
                                    {
                                        wait = true;
                                    }
                                }
                            }
                        }
                    }
                    if(wait==true)
                    {
                        projectComp.project.status = "Wait";
                        Log log = new Log()
                        {
                            projectID = projectComp.project.projectID,
                            status = projectComp.project.status,
                            timestamp = DateTime.Now.ToString()
                        };
                        var logContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(log), System.Text.Encoding.UTF8, "application/json");
                        response = await client.PostAsync($"api/Log/AddLog", logContent);
                    }
                    else
                    {
                        projectComp.project.status = "Scheduled";
                        Log log = new Log()
                        {
                            projectID = projectComp.project.projectID,
                            status = projectComp.project.status,
                            timestamp = DateTime.Now.ToString()

                        };
                        var logContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(log), System.Text.Encoding.UTF8, "application/json");
                        response = await client.PostAsync($"api/Log/AddLog", logContent);
                    }
                    var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(projectComp.project), System.Text.Encoding.UTF8, "application/json");
                    response = await client.PostAsync($"api/Project/ChangeStatus", content);

                    content = new StringContent(System.Text.Json.JsonSerializer.Serialize(projectComp.project), System.Text.Encoding.UTF8, "application/json");
                    response = await client.PostAsync($"api/Project/ChangePrice", content);

                }
            }

        }
    }
}
