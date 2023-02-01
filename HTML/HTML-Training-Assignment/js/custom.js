const menuBar = document.querySelector("#menu-bar");
const sideBar = document.querySelector("aside");

menuBar.addEventListener("click", () => {
  sideBar.classList.toggle("hide-aside");
});
const tab1 = document.querySelector("#tab-1");
const tab2 = document.querySelector("#tab-2");
const tab3 = document.querySelector("#tab-3");

const text = `Lorem, ipsum dolor sit amet consectetur adipisicing elit. Dignissimos, et aliquam
                                    exercitationem doloribus itaque ut necessitatibus magnam alias optio delectus laudantium
                                    dicta odio ipsum eius asperiores voluptatibus error tempore quia ea provident sed
                                    officiis perspiciatis molestias ab. Mollitia autem quasi neque! Aliquid animi est
                                    maiores ipsa eum, quaerat id magni!`;
const tabContent = document.querySelector(".tab-text");
tabContent.textContent = "Tab1" + text;
tab1.classList.add("custom-active");
tab1.addEventListener("click", () => {
  tabContent.textContent = "Tab 1" + text;
  tab1.classList.add("custom-active");
  tab2.classList.remove("custom-active");
  tab3.classList.remove("custom-active");
});
tab2.addEventListener("click", () => {
  tab2.classList.add("custom-active");
  tabContent.textContent = "Tab2 " + text;
  tab1.classList.remove("custom-active");
  tab3.classList.remove("custom-active");
});
tab3.addEventListener("click", () => {
  tab3.classList.add("custom-active");
  tabContent.textContent = "Tab 3" + text;
  tab1.classList.remove("custom-active");
  tab2.classList.remove("custom-active");
});
