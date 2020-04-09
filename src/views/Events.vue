<template>
  <div class="main-wrapper">
    <div class="section events causes">
      <div class="container">
        <div class="power_heading_wrapper text-center">
          <div class="power_heading_icon">
            <img src="images/header_icon1.png" alt="" />
          </div>
          <h1 class="power_title ">Upcoming Event</h1>
          <div class="power_title_line"></div>
        </div>
        <div class="upcoming_event">
          <div class="upcoming_event_wrapper">
            <div class="upcoming_event_image">
              <img class="primary_img" src="images/events/large_1.jpg" alt="" />
            </div>
            <div class="upcoming_event_content">
              <div class="row">
                <div class="col-lg-8 offset-lg-2">
                  <div id="clockdiv">
                    <div class="fifth">
                      <span class="days">110</span>
                      <div class="smalltext">Days</div>
                    </div>
                    <div class="first">
                      <span class="hours">21</span>
                      <div class="smalltext">Hours</div>
                    </div>
                    <div class="second">
                      <span class="minutes">32</span>
                      <div class="smalltext">Minutes</div>
                    </div>
                    <div class="third">
                      <span class="seconds">11</span>
                      <div class="smalltext">Seconds</div>
                    </div>
                  </div>
                  <div class="upcoming_event_content_inner">
                    <div class="upcoming_event_content_left">
                      <div class="highlight_tag">Latest Event</div>
                      <h3>
                        <a href="#"
                          >Annual Volunteer meetup<br />
                          in New York</a
                        >
                      </h3>
                      <h6 class="upcoming_event_location">
                        <i class="ion-ios-pin"></i> 23/4 RK Street, United
                        States of America
                      </h6>
                    </div>
                    <div class="upcoming_event_content_right">
                      <div class="power_button_group">
                        <router-link
                          :to="{ name: 'EventSignup' }"
                          class="power_button effect_1"
                        >
                          <span class="button_value">Register Now!</span>
                        </router-link>
                      </div>
                      <p><span>10</span> seats left only. So hurry !!</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="section causes bg_color_1">
        <div class="container">
          <div class="power_heading_wrapper text-center">
            <div class="power_heading_icon">
              <img src="images/header_icon1.png" alt="" />
            </div>
            <h1 class="power_title ">Latest Events</h1>
            <div class="power_title_line"></div>
          </div>
          <div class="row">
            <div
              v-for="{
                id,
                title,
                description,
                type,
                totalSignup,
                currentSignup,
                color,
                image
              } in cards"
              :key="id"
              class="col-lg-4 col-md-6"
            >
              <div :class="['power_causes', color]">
                <div class="power_causes_wrapper">
                  <div class="power_causes_image">
                    <img class="primary_img" :src="getImgUrl(image)" alt="" />
                    <div class="power_highlight_tag">{{ type }}</div>
                  </div>
                  <div class="power_causes_content">
                    <h4>{{ title }}</h4>
                    <p>
                      {{ description }}
                    </p>
                    <div class="power_progress_content">
                      <div class="power_progress_bar_back">
                        <div
                          class="power_progress_bar"
                          :style="{
                            'max-width':
                              (currentSignup / totalSignup) * 100 + '%;'
                          }"
                        >
                          <span class="power_progress_value"
                            >{{ (currentSignup / totalSignup) * 100 }}%</span
                          >
                        </div>
                      </div>
                      <div class="power_progress_amount">
                        <span
                          ><i class="ion-md-wifi"></i> <b>Places Available:</b>
                          {{ totalSignup }}</span
                        >
                        <span
                          ><i class="ion-logo-usd"></i> <b>Places Taken:</b>
                          {{ currentSignup }}</span
                        >
                      </div>
                    </div>
                  </div>
                  <div class="power_button_group">
                    <router-link
                      :to="{ name: 'EventSignup' }"
                      class="power_button effect_1"
                    >
                      <span class="button_value">Register Now!</span>
                    </router-link>
                    <a href="#" class="power_button effect_3">
                      <span class="button_value">Learn More</span>
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      cards: [
        {
          id: 1,
          title: "Food nutrition program",
          description:
            "Lorem ipsum dolor sit amet consectetur adipiscing rhoncus nisi.",
          type: "Student Only",
          totalSignup: 30,
          currentSignup: 15,
          color: "first",
          image: "1.jpg"
        },
        {
          id: 2,
          title: "Food nutrition program",
          description:
            "Lorem ipsum dolor sit amet consectetur adipiscing rhoncus nisi.",
          type: "Student Only",
          totalSignup: 40,
          currentSignup: 30,
          color: "second",
          image: "2.jpg"
        },
        {
          id: 3,
          title: "Food nutrition program",
          description:
            "Lorem ipsum dolor sit amet consectetur adipiscing rhoncus nisi.",
          type: "Student Only",
          totalSignup: 40,
          currentSignup: 36,
          color: "third",
          image: "5.jpg"
        },
        {
          id: 4,
          title: "Food nutrition program",
          description:
            "Lorem ipsum dolor sit amet consectetur adipiscing rhoncus nisi.",
          type: "Student Only",
          totalSignup: 30,
          currentSignup: 15,
          color: "fourth",
          image: "5.jpg"
        }
      ]
    };
  },
  methods: {
    getImgUrl(img) {
      return "images/causes/" + img;
    }
  }
};
</script>
