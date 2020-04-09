<template>
  <main>
    <div class="main-wrapper">
      <HomeHero />
      <HomeEvents />
      <LatestNews />
      <NewsLetterSignup />
    </div>
  </main>
</template>

<script>
import HomeHero from "@/components/home/HomeHero";
import LatestNews from "@/components/home/LatestNews";
import HomeEvents from "@/components/home/HomeEvents";
import NewsLetterSignup from "@/components/home/NewsLetterSignup";
export default {
  name: "Home",
  components: {
    HomeHero,
    LatestNews,
    HomeEvents,
    NewsLetterSignup
  }
};
</script>
