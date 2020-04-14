<template>
  <div class="section events">
    <div class="container">
      <div class="power_heading_wrapper text-center">
        <h1 class="power_title ">
          Through the year 2020 <br />
          we've bunch of <span class="primary-color">charity events</span>
        </h1>
        <div class="power_title_line"></div>
      </div>
      <div class="row">
        <div class="col-lg-4 col-md-6">
          <div class="power_events">
            <div class="power_events_wrapper">
              <div class="power_events_image">
                <img
                  class="primary_img"
                  :src="'/images/' + eventData[3].image"
                  alt=""
                />
                <div class="power_events_date">
                  <span class="day">04</span><span class="month">May</span>
                </div>
              </div>
              <div class="power_events_content">
                <h3>
                  <a href="#">{{ eventData[3].title }}</a>
                </h3>
                <p>
                  {{ eventData[3].description }}
                </p>
                <div class="power_events_bottom">
                  <router-link
                    :to="{
                      name: 'EventSignup',
                      query: { id: eventData[3].id }
                    }"
                  >
                    Free Booking
                  </router-link>
                  <span>{{ seatsLeft(eventData[3]) }} seats left</span>
                </div>
                <h6 class="power_events_location">
                  <i class="ion-ios-pin"></i> 23/4 RK Street, United States of
                  America
                </h6>
              </div>
            </div>
          </div>
        </div>
        <div class="col-lg-4 col-md-6">
          <div class="power_events">
            <div class="power_events_wrapper">
              <div class="power_events_image">
                <img
                  class="primary_img"
                  :src="'/images/' + eventData[1].image"
                  alt=""
                />
                <div class="power_events_date">
                  <span class="day">02</span><span class="month">May</span>
                </div>
              </div>
              <div class="power_events_content">
                <h3>
                  <a href="#">{{ eventData[1].title }}</a>
                </h3>
                <p>
                  {{ eventData[1].description }}
                </p>
                <div class="power_events_bottom">
                  <router-link
                    :to="{
                      name: 'EventSignup',
                      query: { id: eventData[1].id }
                    }"
                  >
                    Book Now
                  </router-link>
                  <span>{{ seatsLeft(eventData[1]) }} seats left</span>
                </div>
                <h6 class="power_events_location">
                  <i class="ion-ios-pin"></i> 23/4 RK Street, United States of
                  America
                </h6>
              </div>
            </div>
          </div>
        </div>
        <div class="col-lg-4 col-md-6">
          <div class="power_events_list">
            <div class="power_events_list_wrapper">
              <div class="power_events_list_image">
                <img class="primary_img" src="@/assets/food.png" alt="" />
              </div>
              <div class="power_events_list_content">
                <h6><a href="#">Healthy food campaign for children</a></h6>
                <div class="power_events_list_date">15 April 2020</div>
              </div>
            </div>
          </div>

          <div class="power_events_list">
            <div class="power_events_list_wrapper">
              <div class="power_events_list_image">
                <img class="primary_img" src="@/assets/hands.png" alt="" />
              </div>
              <div class="power_events_list_content">
                <h6>
                  <a href="#">Fund collection for cancer research team</a>
                </h6>
                <div class="power_events_list_date">21st February 2020</div>
              </div>
            </div>
          </div>

          <div class="power_events_list">
            <div class="power_events_list_wrapper">
              <div class="power_events_list_image">
                <img class="primary_img" src="@/assets/school.png" alt="" />
              </div>
              <div class="power_events_list_content">
                <h6>
                  <a href="#">Challenges we face to establish school</a>
                </h6>
                <div class="power_events_list_date">19th February 2020</div>
              </div>
            </div>
          </div>

          <div class="power_events_list">
            <div class="power_events_list_wrapper">
              <div class="power_events_list_image">
                <img class="primary_img" src="@/assets/run.png" alt="" />
              </div>
              <div class="power_events_list_content">
                <h6>
                  <a href="#">Charity marathon race event, California</a>
                </h6>
                <div class="power_events_list_date">22nd January 2020</div>
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
  computed: {
    eventData: function() {
      return this.$store.getters.eventData;
    }
  },
  methods: {
    seatsLeft: function(event) {
      return event.totalSignup - event.currentSignup;
    },
    description: function(event) {
      return event.description;
    },
    title: function(event) {
      return event.title;
    }
  }
};
</script>
<style lang="sass" scoped>
.power_events_image
  img
    overflow: hidden
    max-height: 212px
.power_events_list_image
  img
    height: 93px
    width: 145px
    overflow: hidden
</style>
