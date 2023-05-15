import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      component: () => import("../views/HomeView.vue"),
    },
    {
      path: "/about",
      name: "about",
      component: () => import("../views/AboutView.vue"),
    },
    {
      path: "/stations",
      name: "stations",
      component: () => import("../views/StationsView.vue"),
    },
    {
      path: "/roads",
      name: "roads",
      component: () => import("../views/RoadsView.vue"),
    },
    {
      path: "/trains",
      name: "trains",
      component: () => import("../views/TrainsView.vue"),
    },
    {
      path: "/engineTypes",
      name: "engineTypes",
      component: () => import("../views/EngineTypesView.vue"),
    },
    {
      path: "/carTypes",
      name: "carTypes",
      component: () => import("../views/CarTypesView.vue"),
    },
    {
      path: "/engines",
      name: "engines",
      component: () => import("../views/EnginesView.vue"),
    },
    {
      path: "/cars",
      name: "cars",
      component: () => import("../views/CarsView.vue"),
    },
    {
      path: "/trains",
      name: "trains",
      component: () => import("../views/TrainsView.vue"),
    },
    {
      path: "/journeys",
      name: "journeys",
      component: () => import("../views/JourneysView.vue"),
    },
    {
      path: "/map",
      name: "map",
      component: () => import("../views/MapView.vue"),
    },
    {
      path: "/register",
      name: "register",
      component: () => import("../views/RegisterView.vue"),
    },
    {
      path: "/login",
      name: "login",
      component: () => import("../views/LoginView.vue"),
    },
  ],
});

export default router;
