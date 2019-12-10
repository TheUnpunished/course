import Vue from 'vue';
import Vuex from 'vuex';
import api from './api';
import cookies from 'js-cookie'
import {wait} from "./utils";
import router from "./router"
import deleteCookies from 'js-cookie-remove-all'

Vue.use(Vuex);

export default new Vuex.Store({
	state: {
    projects: [],
    email: "",
    people: [],
    tasks: [],
    springs: [],
    logs: [],
    gitData: [],
    dates: [],
    counts: []
	},
	getters: {
	},
	mutations: {
    'SET_PROJECTS'(state, projects){
	    state.projects = projects;
    },
    'SET_USER'(state, email){
	    state.email = email;
    },
    'SET_PEOPLE'(state, people){
	    state.people = people;
    },
    'SET_TASKS'(state, tasks){
	    state.tasks = tasks;
    },
    'SET_SPRINGS'(state, springs){
	    state.springs = springs;
    },
    'SET_LOGS'(state, logs){
	    state.logs = logs;
    },
    'SET_GIT_DATA'(state, gitData){
	    state.gitData = gitData;
    },
    'SET_DATES'(state, dates){
      state.dates = dates;
    },
    'SET_COUNTS'(state, counts){
      state.counts = counts;
    }
	},
	actions: {
		async authorise(context, loginForm){
		  try{
        let userList = await api.user.getAll();
        let users = userList.data;
        users = users.filter(function (elem) {
          return (elem.email.toUpperCase() === loginForm.email.toUpperCase() && elem.password === loginForm.password);
        });
        let user = users[0];
        console.log(users);
        console.log(loginForm.email);
        console.log(loginForm.password);
        if(user !== undefined && users.length >0){
          if(user.email == loginForm.email &&
          user.password == loginForm.password){
            cookies.set('userId', user.id);
          }
          else {
            window.alert("Такой комбинации не существует");
          }
        }
        else{
          window.alert("Такой комбинации не существует");
        }
      }
      catch (e) {
        window.alert("Такой комбинации не существует, либо данные неправильно введены");
      }
    },
    async register(context, regForm){
		  try{
		    let userList = await api.user.getAll();
		    let users = userList.data;
        users = users.filter(function (elem) {
          return elem.email.toUpperCase().indexOf(regForm.email.toUpperCase()) > -1;
        });
        if (users == undefined || users.length == 0){
          await api.user.add({id: undefined, email: regForm.email, password: regForm.password});
          userList = await api.user.getAll();
          users = userList.data;
          users = users.filter(function (elem) {
            return elem.email.toUpperCase().indexOf(regForm.email.toUpperCase()) > -1;
          });
          let userId = users[0].id;
          await api.roles.add({id: userId, status: "Admin"});
        }
        else{
          window.alert("Такой пользователь уже существует");
        }
      }
      catch (e) {
        window.alert("Произошла ошибка. Попробуйте ещё раз.");
      }
    },
    async setProjects(context){
      try{
        let projectData = await api.project.getAll();
        let projects = projectData.data;
        let id = cookies.get("userId");
        projects = projects.filter(function (elem) {
          return elem.userid == id || elem.isPublic;
        });
        context.commit('SET_PROJECTS', projects);
      }
      catch (e) {
        window.alert("Произошла ошибка при получении данных.")
      }
    },
    async setProjectId(context, id){
		  cookies.set("projectId", id);
    },
    async deleteProject(context, id){
		  try{
        if(window.confirm("Вы точно хотите удалить этот проект?")){
          await api.project.delete(id);
        }
      }
      catch (e) {
        window.alert("Произошла ошибка при удалении");
      }
    },
    async setEmail(context){
		  try{
		    let id = cookies.get("userId");
		    if(id != undefined){
		      let userData = await api.user.getOne(id);
		      let user = userData.data;
		      let email = user.email;
		      context.commit('SET_USER', email);
        }
      }
      catch (e) {
        window.alert("Произошла ошибка при получении данных");
      }
    },
    async logout(context){
		  if(window.confirm("Вы точно хотите выйти?")) {
        context.commit('SET_USER', "");
        deleteCookies.removeAll();
      }
    },
    async setPeople(context){
		  try{
		    let peopleData = await api.people.getAll();
		    let people = peopleData.data;
		    context.commit('SET_PEOPLE', people);
      }
      catch (e) {
        window.alert("Произошла ошибка при получении данных");
      }
    },
    async addPeople(context, peopleForm){
		  try {
        if (peopleForm.admin) {
          let peopleData = await api.people.getAll();
          let people = peopleData.data;
          people = people.filter(function (elem) {
            return elem.email.toUpperCase() == peopleForm.email.toUpperCase();
          });
          if (people == undefined || people.length == 0) {
            await api.people.add({
              user: peopleForm.email, name: peopleForm.name,
              surname: peopleForm.surname, email: peopleForm.email, language: peopleForm.language,
              admin: peopleForm.admin, password: peopleForm.password
            });
            await wait(1000);
            peopleData = await api.people.getAll();
            people = peopleData.data;
            people = people.filter(function (elem) {
              return elem.email.toUpperCase() == peopleForm.email.toUpperCase();
            });
            let person = people[0];
            await api.category.add({name: peopleForm.categoryName, people_id: person, peopleid: person.id});
          } else {
            window.alert("Произошла ошибка. Возможно, такой пользователь уже существует");
          }
        } else {
          let userList = await api.user.getAll();
          let users = userList.data;
          users = users.filter(function (elem) {
            return elem.email.toUpperCase() == peopleForm.email.toUpperCase();
          });
          if ((users == undefined || users.length == 0)) {
            await api.user.add({email: peopleForm.email, password: peopleForm.password});
            userList = await api.user.getAll();
            users = userList.data;
            users = users.filter(function (elem) {
              return elem.email.toUpperCase() == peopleForm.email.toUpperCase();
            });
            let userId = users[0].id;
            await api.roles.add({id: userId, status: peopleForm.status});
            await api.people.add({
              user: peopleForm.email, name: peopleForm.name,
              surname: peopleForm.surname, email: peopleForm.email, language: peopleForm.language,
              admin: peopleForm.admin, password: peopleForm.password
            });
            await wait(1000);
            let peopleData = await api.people.getAll();
            let people = peopleData.data;
            people = people.filter(function (elem) {
              return elem.email.toUpperCase() == peopleForm.email.toUpperCase();
            });
            let person = people[0];
            await api.category.add({name: peopleForm.categoryName, people_id: person, peopleid: person.id});
          } else {
            window.alert("Произошла ошибка. Возможно, такой пользователь уже существует");
          }
        }
      }
      catch (e) {
      }
    },
    async getCategory(context, id){
		  try{
		    let categoryData = await api.category.getOne(id);
		    let category = categoryData.data;
		    return category.name
      }
      catch (e) {
        window.alert("Произошла ошибка")
      }
    },
    async addProject(context, projectForm){
      try{
        let userId = cookies.get("userId");
        await api.project.add({name: projectForm.name, description: projectForm.description,
          isPublic: projectForm.public, userid: userId, repository: projectForm.repo});
      }
      catch (e) {
        window.alert("Произошла ошибка");
      }
    },
    async setTasks(context){
		  try{
		    let userId = cookies.get("userId");
        let roleData = await api.roles.getOne(userId);
        let role = roleData.data;
		    if (role.status == "Worker"){
          let userId = cookies.get("userId");
          let userData = await api.user.getOne(userId);
          let user = userData.data;
          let peopleData = await api.user.getAll();
          let people = peopleData.data;
          people = people.filter(function (elem) {
            return elem.email.toUpperCase().indexOf(user.email.toUpperCase()) > -1
          });
          let person = people[0];
          let tasksData = await api.tasks.getAll();
          let tasks = tasksData.data;
          tasks = tasks.filter(function (elem) {
            return elem.whoCan.id == person.id;
          });
          context.commit('SET_TASKS', tasks);
        }
		    else{
          let taskData = await api.tasks.getAll();
          let tasks = taskData.data;
          let projectId = cookies.get("projectId");
          tasks = tasks.filter(function (elem) {
            return elem.project_id.id == projectId;
          });
          console.log(tasks);
          context.commit('SET_TASKS', tasks);
        }
      }
      catch (e) {

      }
    },
    async setTasksCookies(context){
      try{
        let taskData = await api.tasks.getAll();
        let tasks = taskData.data;
        let projectId = cookies.get("projectId");
        tasks = tasks.filter(function (elem) {
          return elem.project_id.id === projectId;
        });
        context.commit('SET_TASKS', tasks);
      }
      catch (e) {
        window.alert("Произошла ошибка")
      }
    },
    async getProject(context, id){
		  try{
        let projectData = await api.project.getOne(id);
        let newProject = projectData.data;
        console.log(newProject);
        return newProject;
      }
		  catch (e) {
        window.alert("Произошла ошибка");
      }
    },
    async deleteTask(context, id){
		  if(window.prompt("Вы точно хотите удалить данное задание?"))
		  await api.tasks.delete(id);
    },
    async setTaskId(context, id){
		  cookies.set("taskId", id);
    },
    async setSprings(context){
		  try{
        let springData = await api.spring.getAll();
        let springs = springData.data;
        context.commit('SET_SPRINGS', springs);
      }
		  catch (e) {
        window.alert("Произошла ошибка")
      }
    },
    async editProject(context, obj){
		  let userId = cookies.get("userId");
      await api.project.edit(obj.id, {name: obj.name,
      description: obj.description, userid: userId,
        isPublic: obj.isPublic, repository: obj.repository});
    },
    setPersonId(context, id){
		  cookies.set("personId", id);
    },
    async getPersonById(context, id) {
      let personData = await api.people.getOne(id);
      let person = personData.data;
      return person;
    },
    async getCategoryById(context, id){
		  let categoryData = await api.category.getAll();
		  let categories = categoryData.data;
		  categories = categories.filter(function (elem) {
        return elem.people_id.id == id;
      });
		  return categories[0];
    },
    async addSpring(context, springForm){
      const date = new Date();
      await api.wiki.add({
        name: springForm.wikiName,
        description: springForm.wikiDesc,
        file_path: springForm.wikiFPath
      });
        let wikiData = await api.wiki.getAll();
        let wikis = wikiData.data;
        let wiki = wikis[wikis.length-1];
        let wikiId = wiki.id;
      await api.spring.add({
        name: springForm.name,
        description: springForm.description,
        date: date,
        status: springForm.status,
        wikiid: wikiId,
        wiki: wiki
      });
    },
    async setSpringId(context, id){
		  cookies.set("springId", id);
    },
    async getSpring(context, id){
		  let springData = await api.spring.getOne(id);
		  let spring = springData.data;
		  return spring;
    },
    async editSpring(context, springForm){
		  await api.wiki.edit(springForm.wikiId,{
		    name: springForm.wikiName,
        description: springForm.wikiDesc,
        file_path: springForm.wikiFPath
      });
		  await api.spring.edit(springForm.springId,
        {name: springForm.name,
        description: springForm.description,
        date: springForm.springDate,
        wikiid: springForm.wikiId,
          wiki_id: {id: springForm.wikiId,
          name: springForm.wikiName,
          description: springForm.description,
          file_path: springForm.wikiFPath},
          status: springForm.status
        });
    },
    async getWiki(context, id){
		  let springData = await api.spring.getOne(id);
		  let springs = springData.data;
		  let thisWiki = springs.wiki;
		  let wikiId = thisWiki.id;
		  let wikiData = await api.wiki.getOne(wikiId);
		  let wiki = wikiData.data;
		  return wiki;
    },
    async addTask(context, taskForm){
		  await api.tracker.add({name:taskForm.trackerName,
      status: taskForm.trackerStat});
		  let trackerData = await api.tracker.getAll();
		  let trackers = trackerData.data;
		  let tracker = trackers[trackers.length-1];
		  let peopleData = await api.people.getOne(taskForm.whoCan);
		  let person = peopleData.data;
		  let springData = await api.people.getOne(taskForm.spring);
		  let spring = springData.data;
      let categoryData = await api.category.getAll();
      let categories = categoryData.data;
      categories = categories.filter(function (elem) {
        return elem.people_id.id == person.id;
      });
      let category = categories[0];
		  let projectData = await api.project.getOne(taskForm.project_id);
		  let project = projectData.data;
		  await api.tasks.add({
        theme: taskForm.theme,
        trackerid: tracker.id,
        tracker: tracker,
        status: taskForm.trackerStat,
        priority: taskForm.priority,
        description: taskForm.description,
        categoryid: category.id,
        category: category,
        springid: taskForm.spring,
        spring: spring,
        date_over: taskForm.date_over,
        time: taskForm.time,
        percent: taskForm.percent,
        file_path: taskForm.file_path,
        whocanid: taskForm.whoCan,
        whoCan: person,
        project_id: project,
        projectid: taskForm.project_id
      });
		  let userId = cookies.get("userId");
		  let userData = await api.user.getOne(userId);
		  let user = userData.data;
		  let taskData = await api.tasks.getAll();
		  let tasks = taskData.data;
		  let task = tasks[tasks.length-1];
		  await api.logs.add({
        name: "Новая задача",
        taskid: task.id,
        task_id: task,
        description: "Пользователь " + user.email + " добавил новое задание " + task.theme,
        file_path: taskForm.file_path
      });
		  await api.sLogs.add({
        time: new Date(),
        taskid: task.id,
        task_id: undefined
      });
    },
    async editTask(context, taskForm){
      await api.tracker.edit(taskForm.trackerId, {name: taskForm.trackerName,
      status: taskForm.trackerStat});
      let categoryData = await api.category.getAll();
      let categories = categoryData.data;
      categories = categories.filter(function (elem) {
        return elem.people_id.id == taskForm.whoCan;
      });
      let category = categories[0];
      let springData = await api.spring.getOne(taskForm.spring);
      let spring = springData.data;
      let peopleData = await api.people.getOne(taskForm.whoCan);
      let person = peopleData.data;
      let projectData = await api.project.getOne(taskForm.project_id);
      let project = projectData.data;
      let tracker = {id: taskForm.trackerId,
      name: taskForm.trackerName,
      status: taskForm.trackerStat};
      await api.tasks.edit(taskForm.id,{
        theme: taskForm.theme,
        trackerid: tracker.id,
        tracker: tracker,
        status: tracker.status,
        priority: taskForm.priority,
        description: taskForm.description,
        categoryid: category.id,
        category: category,
        springid: taskForm.spring,
        spring: spring,
        date_over: taskForm.date_over,
        time: taskForm.time,
        percent: taskForm.percent,
        file_path: taskForm.file_path,
        whocanid: taskForm.whoCan,
        whoCan: person,
        project_id: project,
        projectid: project.id
      });
      let userId = cookies.get("userId");
      let userData = await api.user.getOne(userId);
      let user = userData.data;
      await api.logs.add({
        name: "Изменение задачи",
        taskid: taskForm.id,
        task_id: undefined,
        description: "Пользователь " + user.email + " изменил задачу " + taskForm.theme,
        file_path: taskForm.file_path
      });
      if(tracker.status == "Closed"){
        await api.sLogs.add({
          time: new Date(),
          taskid: taskForm.id,
          task_id: undefined,
        });
      }
    },
    async getTask(context, id){
      let taskData = await api.tasks.getOne(id);
      let task = taskData.data;
      return task;
    },
    async setLogs(context){
		  let logsData = await api.logs.getAll();
		  let logs = logsData.data;
		  context.commit('SET_LOGS', logs);
    },
    async getTracker(context, id){
		  let trackerData = await api.tracker.getOne(id);
		  let tracker = trackerData.data;
		  return tracker;
    },
    async getRole(context){
		  let userId = cookies.get("userId");
		  if (userId!=undefined){
        let roleData = await api.roles.getOne(userId);
        let role = roleData.data;
        return role.status;
      }
		  else{
		    return 'Undefined';
      }
    },
    async completeTask(context, completeForm){
		  let taskData = await api.tasks.getOne(completeForm.id);
		  let task = taskData.data;
		  task.file_path = completeForm.file_path;
		  task.status = "Closed";
		  let tracker = task.tracker;
		  let trackerData = await api.tracker.getOne(tracker.id);
		  tracker = trackerData.data;
		  tracker.status = "Closed";
		  task.tracker = tracker;
		  task.trackerid = tracker.id;
		  let categoryData = await api.category.getOne(task.category.id);
		  let category = categoryData.data;
		  task.categoryid = category.id;
		  task.category = category;
		  let projectData = await api.project.getOne(task.project_id.id);
		  let project = projectData.data;
		  task.projectid = project.id;
		  task.project_id = project;
		  let springData = await api.spring.getOne(task.spring.id);
		  let spring = springData.data;
		  task.springid = spring.id;
		  task.spring = spring;
		  let whoCanData = await api.people.getOne(task.whoCan.id);
		  let whoCan = whoCanData.data;
		  task.whoCan = whoCan;
		  task.whocanid = whoCan.id;
		  await api.tasks.edit(completeForm.id, task);
		  let userId = cookies.get("userId");
		  let userData = await api.user.getOne(userId);
		  let user = userData.data;
		  await api.logs.add({
        name: "Завершение задачи",
        taskid: completeForm.id,
        task_id: undefined,
        description: "Пользователь " + user.email + " закрыл задачу " + task.theme,
        file_path: completeForm.file_path
      });
		  await api.sLogs.add({
        time: new Date(),
        taskid: completeForm.id,
        task_id: undefined,
      });
    },
    async setCommits(context){
		  let projectId = cookies.get("projectId");
		  console.log(projectId);
		  let projectData = await api.project.getOne(projectId);
		  let project = projectData.data;
		  let gitData = await api.gitHub.getAll(project.repository);
      context.commit('SET_GIT_DATA', gitData);
    },
    async getRoleByLogin(context, email){
		  let userData = await api.user.getAll();
		  let users = userData.data;
		  users = users.filter(function (elem) {
        return elem.email.toUpperCase() == email.toUpperCase()
      });
		  let roleName = "Manager";
		  if (users != undefined && users.length > 0){
		    let userId = users[0].id;
		    let roleData = await api.roles.getOne(userId);
		    let role = roleData.data;
		    roleName = role.status;
      }
		  return roleName;
    },
    async getTasksBySpring(context, id){
		  let taskData = await api.tasks.getAll();
		  let tasks = taskData.data;
		  tasks = tasks.filter(function (elem) {
        return elem.spring.id == id;
      });
		  return tasks;
    },
    async setPeopleExceptId(context, id){
      try{
        let peopleData = await api.people.getAll();
        let people = peopleData.data;
        people = people.filter(function (elem) {
          return elem.id != id;
        });
        context.commit('SET_PEOPLE', people);
      }
      catch (e) {
        window.alert("Произошла ошибка при получении данных");
      }
    },
    async getDiagramData(context, datePair){
		  let data = [];
		  let dates = [];
		  let dateOne = "";
		  let dateTwo = "";
		  if (datePair.dateOne > datePair.dateTwo){
		    dateOne = new Date(datePair.dateTwo);
		    dateTwo = new Date(datePair.dateOne);
      }
		  else{
		    dateOne = new Date(datePair.dateOne);
		    dateTwo = new Date(datePair.dateTwo);
      }
		  let sLogData = await api.sLogs.getAll();
		  let slogs = sLogData.data;
		  let taskData = await api.tasks.getAll();
		  let tasks = taskData.data;
		  let projectId = cookies.get("projectId");
      tasks = tasks.filter(function (elem) {
        return elem.project_id.id == projectId;
      });
      let slogs_temp = slogs;
      let tasks_temp = tasks;
      while (dateOne <= dateTwo){
        let count = 0;
        let slogs_date = slogs.filter(function (elem) {
          return new Date(elem.time) <= dateOne;
        });
        count = slogs_date.length;
        for (let i =0; i < slogs_date.length; i++){
          let tasks_date = tasks.filter(function (elem) {
            return elem.id == slogs_date[i].taskid;
          });
          if (tasks_date.length > 0){
            let task = tasks_date[0];
            let status = task.status;
            if(status == "Closed"){
              count -= 1;
            };
          }
        }
        data.push(count);
        dates.push(dateOne.toDateString());
        dateOne.setDate(dateOne.getDate() + 1);
        slogs = slogs_temp;
        tasks = tasks_temp;
      }
      context.commit('SET_DATES', dates);
      context.commit('SET_COUNTS', data);
      console.log(dates);
      console.log(data);
    },
    async unauthorised(context, level){
		  const userId = cookies.get("userId");
		  if (userId == undefined){
		    router.push({name:"loader"});
      }
		  else{
		    let roleData = await api.roles.getOne(userId);
		    let role = roleData.data;
		    switch (level) {
          case 0:{
            if (role.status != "Admin" && role.status != "Manager" && role.status != "Worker"){
              router.push({name: "loader"});
            }
            break;
          }
          case 1:{
            if (role.status != "Admin" && role.status != "Manager"){
              router.push({name: "loader"});
            }
            break;
          }
          case 2:{
            if (role.status != "Admin"){
              router.push({name: "loader"});
            }
            break;
          }
        }
      }
    }
	}
});
