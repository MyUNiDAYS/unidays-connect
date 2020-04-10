<template>
  <div class="col-lg-4 col-md-6">
    <div :class="['power_causes', eventDetails.color]">
      <div class="power_causes_wrapper">
        <div class="power_causes_image">
          <img
            class="primary_img"
            :src="getImgUrl(eventDetails.image)"
            alt=""
          />
          <div class="power_highlight_tag">{{ eventDetails.type }}</div>
        </div>
        <div class="power_causes_content">
          <h4>{{ eventDetails.title }}</h4>
          <p>
            {{ eventDetails.description }}
          </p>
          <div class="power_progress_content">
            <div class="power_progress_bar_back">
              <div
                class="power_progress_bar"
                :style="{
                  maxWidth:
                    (eventDetails.currentSignup / eventDetails.totalSignup) *
                      100 +
                    '%'
                }"
              >
                <span class="power_progress_value"
                  >{{
                    (eventDetails.currentSignup / eventDetails.totalSignup) *
                      100
                  }}%</span
                >
              </div>
            </div>
            <div class="power_progress_amount">
              <span
                ><i class="ion-md-wifi"></i> <b>Places Available:</b>
                {{ eventDetails.totalSignup }}</span
              >
              <span
                ><i class="ion-logo-usd"></i> <b>Places Taken:</b>
                {{ eventDetails.currentSignup }}</span
              >
            </div>
          </div>
        </div>
        <div class="power_button_group">
          <router-link
            :to="{ name: 'EventSignup', query: { id: eventDetails.id } }"
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
</template>
<script>
export default {
  props: ["eventDetails"],
  methods: {
    getImgUrl(img) {
      return "images/causes/" + img;
    }
  }
};
</script>
