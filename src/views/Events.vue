<template>
  <div class="main-wrapper">
    <div class="section events causes">
      <div class="container">
        <div class="power_heading_wrapper text-center">
          <div class="power_heading_icon">
            <img src="@/assets/images/header_icon1.png" alt="" />
          </div>
          <h1 class="power_title ">Upcoming Event</h1>
          <div class="power_title_line"></div>
        </div>
        <EventHero />
      </div>
      <div class="section causes bg_color_1">
        <div class="container">
          <div class="power_heading_wrapper text-center">
            <div class="power_heading_icon">
              <img src="@/assets/images/header_icon1.png" alt="" />
            </div>
            <h1 class="power_title ">Latest Events</h1>
            <div class="power_title_line"></div>
          </div>
          <div class="row">
            <Event
              v-for="event in eventData"
              :key="event.id"
              :eventDetails="event"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import Event from "@/components/Event";
import EventHero from "@/components/EventHero";
import { mapGetters } from "vuex";
export default {
  components: {
    Event,
    EventHero
  },
  computed: {
    ...mapGetters(["eventData"])
  },
  methods: {
    getImgUrl(img) {
      return "@/assets/images/causes/" + img;
    }
  }
};
</script>
<style lang="sass">
.power_causes_image
  max-height: 212px
  overflow: hidden
</style>
